using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IResumeEditorRepository
    {
        public Task CreateEmptyResume(int userId);

        public Task ChangeGoals(int userId, string goalsText);
    }
}
