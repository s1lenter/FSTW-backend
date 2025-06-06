using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Models;
using FSTW_backend.Repositories.Admin;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using FSTW_backend.Dto.InternshipDto;
using System.Net.Http;

namespace FSTW_backend.Services.Admin
{
    public class AdminService : IAdminService
    {
        IAdminRepository _repository;
        private IMapper _mapper;
        public AdminService(AppDbContext context, IMapper mapper)
        {
            _repository = new AdminRepository(context);
            _mapper = mapper;
        }
        public async Task<ResponseResult<int>> CreateInternship(RequestInternshipDto internshipDto)
        {
            var internship = new Internship();
            _mapper.Map(internshipDto, internship);
            await _repository.CreateInternship(internship);
            return ResponseResult<int>.Success(internship.Id);
        }

        public async Task<ResponseResult<InternshipDto>> GetInternship(int internshipId)
        {
            var internship = await _repository.GetUnarchiveInternship(internshipId);
            if (internship is null)
                return ResponseResult<InternshipDto>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });
            var internshipDto = new InternshipDto();
            _mapper.Map(internship, internshipDto);
            return ResponseResult<InternshipDto>.Success(internshipDto);
        }

        public async Task<ResponseResult<List<InternshipDto>>> GetAllInternships(string filterParam)
        {
            var internships = await _repository.GetAllInternships(filterParam);
            if (internships.Count == 0)
                ResponseResult<List<InternshipDto>>.Success(new List<InternshipDto>());
            var dtosList = new List<InternshipDto>();
            foreach (var internship in internships)
            {
                var dto = new InternshipDto();
                _mapper.Map(internship, dto);
                dtosList.Add(dto);
            }
            return ResponseResult<List<InternshipDto>>.Success(dtosList);
        }

        public async Task<ResponseResult<int>> EditInternship(int internshipId, RequestInternshipDto internshipDto)
        {
            var internship = await _repository.GetUnarchiveInternship(internshipId);

            if (internship is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });

            _mapper.Map(internshipDto, internship);
            await _repository.SaveChangesAsync(internshipId);
            return ResponseResult<int>.Success(internshipId);
        }

        public async Task<ResponseResult<int>> DeleteInternship(int internshipId)
        {
            var internship = await _repository.GetInternship(internshipId);

            if (internship is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });

            await _repository.DeleteInternship(internship);
            return ResponseResult<int>.Success(internshipId);
        }

        public async Task<ResponseResult<int>> ArchiveInternship(int internshipId)
        {
            var internship = await _repository.GetUnarchiveInternship(internshipId);

            if (internship is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });

            await _repository.ArchiveInternship(internship);
            return ResponseResult<int>.Success(internshipId);
        }

        public async Task<ResponseResult<int>> UnarchiveInternship(int internshipId)
        {
            var internship = await _repository.GetInternship(internshipId);

            if (internship is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });

            await _repository.UnarchiveInternship(internship);
            return ResponseResult<int>.Success(internshipId);
        }

        public async Task AddVacanciesFromHh(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.UserAgent
                .ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59");

            var response = await httpClient.GetAsync("https://api.hh.ru/vacancies?L_save_area=true&hhtmFrom=vacancy_search_filter&enable_snippets=true&area=3&experience=noExperience&label=internship&professional_role=156&professional_role=160&professional_role=10&professional_role=12&professional_role=150&professional_role=25&professional_role=165&professional_role=34&professional_role=36&professional_role=73&professional_role=155&professional_role=96&professional_role=164&professional_role=104&professional_role=157&professional_role=107&professional_role=112&professional_role=113&professional_role=148&professional_role=114&professional_role=116&professional_role=121&professional_role=124&professional_role=125&professional_role=126&search_field=name&search_field=company_name&search_field=description&host=hh.ru&full_description=true");
            var content = await response.Content.ReadAsStringAsync();
            HhResponseData jsonData = JsonSerializer.Deserialize<HhResponseData>(content);

            var currInternships = await _repository.GetHhInterships();

            await DeleteOldInternships(jsonData, currInternships);

            var newInternships = new List<Items>();
            foreach (var vac in jsonData.Items)
            {
                var internship = await _repository.GetHhInternship(vac.Id);
                if (internship is null)
                    newInternships.Add(vac);
            }

            await _repository.AddVacanciesFromHh(await CreateNewInterships(httpClient, newInternships));
        }

        private async Task DeleteOldInternships(HhResponseData jsonData, List<Internship> currInternships)
        {
            var newIds = jsonData.Items.Select(i => i.Id);
            var delList = new List<Internship>();

            foreach (var internship in currInternships)
            {
                var isActual = false;
                foreach (var id in newIds)
                {
                    if (id == internship.IdFromHh)
                    {
                        isActual = true;
                        break;
                    }
                }
                if (!isActual)
                    delList.Add(internship);
            }

            if (delList.Count > 0)
                await _repository.DeleteOldInternships(delList);
        }

        private async Task<List<Internship>> CreateNewInterships(HttpClient httpClient, List<Items> newInternships)
        {
            var internshipsList = new List<Internship>();

            foreach (var item in newInternships)
            {
                var itemDirection = InternshipCategory.FindCategory(item.ProfessionalRoles[0].Name);

                var allInfo = await httpClient.GetAsync($"https://api.hh.ru/vacancies/{item.Id}");
                var infoContent = await allInfo.Content.ReadAsStringAsync();

                SkillsInfo skills = JsonSerializer.Deserialize<SkillsInfo>(infoContent);

                item.SkillsInfo = skills;
                if (item.Salary is null)
                    item.Salary = new Salary()
                    {
                        From = null,
                        To = null
                    };

                internshipsList.Add(new Internship()
                {
                    CompanyName = item.Company.Name,
                    Description = item.Info.Description,
                    Direction = itemDirection,
                    Link = item.Link,
                    RequiredSkills = string.Join(", ", skills.Skills.Select(s => s.Skill)),
                    SalaryFrom = item.Salary.From is null ? 0 : item.Salary.From.Value,
                    SalaryTo = item.Salary.To is null ? 0 : item.Salary.To.Value,
                    Title = item.Name,
                    WorkFormat = item.WorkFormats.Count == 0 ? "Не указано" : item.WorkFormats[0].Name,
                    IdFromHh = item.Id
                });
            }
            return internshipsList;
        }
    }
}
