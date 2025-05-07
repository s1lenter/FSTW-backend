using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;
using Profile = FSTW_backend.Models.Profile;

namespace FSTW_backend.Services
{
    public class ResumeEditorService : IResumeEditorService
    {
        private IResumeEditorRepository _repository;
        private IMapper _mapper;
        public ResumeEditorService(AppDbContext appDbContext, IMapper mapper)
        {
            _repository = new ResumeEditorRepository(appDbContext);
            _mapper = mapper;
        }

        public async Task<ResponseResult<int>> SendAboutInfo(int userId, int resumeId, AboutDto aboutDto)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            return await _repository.SendAboutInfo(resume, aboutDto);
        }

        public async Task<ResponseResult<int>> SendExperienceInfo(int userId, int resumeId, string experience)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            return await _repository.SendExperience(resume, experience);
        }

        public async Task<ResponseResult<int>> SendProjects(int userId, int resumeId, List<ProjectDto> projectDtos)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            var projects = new List<Project>();
            foreach (var projectDto in projectDtos)
            {
                var project = new Project();
                _mapper.Map(projectDto, project);
                project.ResumeId = resumeId;
                project.Resume = resume;
                projects.Add(project);
            }
            return await _repository.SendProjects(resume, projects);
        }

        public async Task<ResponseResult<int>> SendAchievements(int userId, int resumeId, List<AchievementDto> achievementDtos)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            var achievements = new List<Achievement>();
            foreach (var achievementDto in achievementDtos)
            {
                var achievement = new Achievement();
                _mapper.Map(achievementDto, achievement);
                achievement.ResumeId = resumeId;
                achievement.Resume = resume;
                achievements.Add(achievement);
            }
            return await _repository.SendAchievements(resume, achievements);
        }

        public async Task<ResponseResult<int>> SendEducation(int userId, int resumeId, List<EducationDto> educationDtos)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            var educations = new List<Education>();
            foreach (var educationDto in educationDtos)
            {
                var education = new Education();
                _mapper.Map(educationDto, education);
                education.ResumeId = resumeId;
                education.Resume = resume;
                educations.Add(education);
            }
            return await _repository.SendEducation(resume, educations);
        }

        public async Task<ResponseResult<int>> SendSkills(int userId, int resumeId, string skills)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            return await _repository.SendSkills(resume, skills);
        }

        public async Task<ResponseResult<AllResumeInfoDto>> GetAllResumeInfo(int userId, int resumeId)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<AllResumeInfoDto>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            var projects = _repository.GetProjects(resumeId);
            var educations = _repository.GetEducations(resumeId);
            var achievements = _repository.GetAchievements(resumeId);

            var profile = await _repository.GetUserProfileAsync(userId);
            var user = await _repository.GetUserAsync(userId);


            return ResponseResult<AllResumeInfoDto>.Success(CreateResumeInfo(user, resume, profile, projects, achievements, educations));
        }

        public async Task<ResponseResult<AllResumeInfoDto>> GetAllResumeInfoForPdf(int userId, int resumeId)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<AllResumeInfoDto>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            var user = await _repository.GetUserAsync(userId);
            var profile = await _repository.GetUserProfileAsync(userId);
            var projects = _repository.GetProjects(resumeId);
            var educations = _repository.GetEducations(resumeId);
            var achievements = _repository.GetAchievements(resumeId);
            return ResponseResult<AllResumeInfoDto>.Success(CreateResumeInfo(user, resume, profile, projects, achievements, educations));
        }

        public async Task<ResponseResult<int>> DeleteResume(int userId, int resumeId)
        {
            var resume = await _repository.GetCurrentResume(userId, resumeId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Резюме с таким Id не существует"}
                });
            await _repository.DeleteResume(resume);
            return ResponseResult<int>.Success(resumeId);
        }

        private List<TResult> MapLists<TSource, TResult>(List<TSource> sourceList) 
            where TResult : new()
        {
            var resultList = new List<TResult>();
            foreach (var item in sourceList)
            {
                var result = new TResult();
                _mapper.Map(item, result);
                resultList.Add(result);
            }

            return resultList;
        }

        public AllResumeInfoDto CreateResumeInfo(User user, Resume resume, Profile profile, List<Project> projects,
            List<Achievement> achievements, List<Education> educations)
        {
            var resumeInfo = new AllResumeInfoDto()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                MiddleName = profile.MiddleName,
                Gender = profile.Gender,
                DateOfBirth = profile.DateOfBirth,
                PhoneNumber = profile.PhoneNumber,
                Vk = profile.Vk,
                Telegram = profile.Telegram,
                GitHub = profile.GitHub,
                Linkedin = profile.Linkedin,
                Email = user.Email,
                Hobbies = resume.Hobbies,
                About = resume.About,
                Experience = resume.Experience,
                Skills = resume.Skills,
                Projects = MapLists<Project, ProjectDto>(projects),
                Educations = MapLists<Education, EducationDto>(educations),
                Achievements = MapLists<Achievement, AchievementDto>(achievements)
            };
            return resumeInfo;
        }


        public async Task<int> CreateEmptyResume(int userId)
        {
            return await _repository.CreateEmptyResume(userId);
        }
    }
}
