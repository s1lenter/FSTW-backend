using FSTW_backend.Dto.ResumeDto;

namespace FSTW_backend.Services
{
    public interface IResumeEditorService
    {
        public Task<int> CreateEmptyResume(int userId);

        public Task<ResponseResult<int>> SendAboutInfo(int userId, int resumeId, AboutDto aboutDto);

        public Task<ResponseResult<int>> SendExperienceInfo(int userId, int resumeId, ExperienceDto experienceDto);
    }
}
