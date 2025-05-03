
using FSTW_backend.Dto;
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

        public async Task SendAboutInfo(int userId, AboutDto aboutDto)
        {
            await _repository.SendAboutInfo(userId, aboutDto);
        }

        public async Task CreateEmptyResume(int userId)
        {
            await _repository.CreateEmptyResume(userId);
        }
    }
}
