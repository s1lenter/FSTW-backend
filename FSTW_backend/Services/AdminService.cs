using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;
using System.Collections.Generic;

namespace FSTW_backend.Services
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
        public async Task<ResponseResult<int>> CreateInternship(InternshipDto internshipDto)
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

        public async Task<ResponseResult<List<InternshipDto>>> GetAllInternships()
        {
            var internships = await _repository.GetAllInternships();
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

        public async Task<ResponseResult<int>> EditInternship(int internshipId, InternshipDto internshipDto)
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
    }
}
