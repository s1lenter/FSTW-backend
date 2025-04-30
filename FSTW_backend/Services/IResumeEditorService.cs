namespace FSTW_backend.Services
{
    public interface IResumeEditorService
    {
        public Task CreateEmptyResume(int userId);

        public Task ChangeGoals(int userId, string goalsText);
    }
}
