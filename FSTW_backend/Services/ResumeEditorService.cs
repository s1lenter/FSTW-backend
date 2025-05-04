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


        public async Task<int> CreateEmptyResume(int userId)
        {
            return await _repository.CreateEmptyResume(userId);
        }
    }
}
