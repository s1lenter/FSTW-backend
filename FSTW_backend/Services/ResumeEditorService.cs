using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Repositories;

namespace FSTW_backend.Services
{
    public class ResumeEditorService : IResumeEditorService
    {
        private IResumeEditorRepository _repository;
        public ResumeEditorService(AppDbContext appDbContext)
        {
            _repository = new ResumeEditorRepository(appDbContext);
        }

        public async Task<ResponseResult<int>> SendAboutInfo(int userId, int resumeId, AboutDto aboutDto)
        {
            return await _repository.SendAboutInfo(userId, resumeId, aboutDto);
        }

        public async Task<ResponseResult<int>> SendExperienceInfo(int userId, int resumeId, ExperienceDto experienceDto)
        {
            return await _repository.SendExperience(userId, resumeId, experienceDto);
        }

        public async Task<ResponseResult<int>> SendProjects(int userId, int resumeId, List<ProjectDto> projectDtos)
        {
            return await _repository.SendProjects(userId, resumeId, projectDtos);
        }

        public async Task<ResponseResult<int>> SendAchievements(int userId, int resumeId, List<AchievementDto> achievementDtos)
        {
            return await _repository.SendAchievements(userId, resumeId, achievementDtos);
        }

        public async Task<ResponseResult<int>> SendEducation(int userId, int resumeId, List<EducationDto> educationDtos)
        {
            return await _repository.SendEducation(userId, resumeId, educationDtos);
        }

        public async Task<ResponseResult<int>> SendSkills(int userId, int resumeId, string skills)
        {
            return await _repository.SendSkills(userId, resumeId, skills);
        }


        public async Task<int> CreateEmptyResume(int userId)
        {
            return await _repository.CreateEmptyResume(userId);
        }
    }
}
