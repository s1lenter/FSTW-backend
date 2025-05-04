using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IResumeEditorRepository
    {
        public Task<int> CreateEmptyResume(int userId);

        public Task<ResponseResult<int>> SendAboutInfo(int userId, int resumeId, AboutDto aboutDto);

        public Task<ResponseResult<int>> SendProjects(int userId, int resumeId, List<ProjectDto> projectDto);

        public Task<ResponseResult<int>> SendExperience(int userId, int resumeId, ExperienceDto experienceDto);
    }
}
