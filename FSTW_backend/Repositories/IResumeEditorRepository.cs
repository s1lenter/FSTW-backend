using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IResumeEditorRepository
    {
        public Task CreateEmptyResume(int userId);

        public Task SendAboutInfo(int userId, AboutDto aboutDto);
    }
}
