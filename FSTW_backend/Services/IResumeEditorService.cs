using FSTW_backend.Dto.ResumeDto;

namespace FSTW_backend.Services
{
    public interface IResumeEditorService
    {
        public Task<int> CreateEmptyResume(int userId);

        public Task<ResponseResult<int>> SendAboutInfo(int userId, int resumeId, AboutDto aboutDto);

        public Task<ResponseResult<int>> SendExperienceInfo(int userId, int resumeId, string experience);

        public Task<ResponseResult<int>> SendProjects(int userId, int resumeId, List<ProjectDto> projectDtos);

        public Task<ResponseResult<int>> SendAchievements(int userId, int resumeId, List<AchievementDto> achievementDtos);

        public Task<ResponseResult<int>> SendEducation(int userId, int resumeId, List<EducationDto> educationDtos);

        public Task<ResponseResult<int>> SendSkills(int userId, int resumeId, string skills);
    }
}
