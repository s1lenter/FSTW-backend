
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

        public async Task ChangeGoals(int userId, string goalsText)
        {
            await _repository.ChangeGoals(userId, goalsText);
        }

        public async Task CreateEmptyResume(int userId)
        {
            await _repository.CreateEmptyResume(userId);
        }
    }
}
