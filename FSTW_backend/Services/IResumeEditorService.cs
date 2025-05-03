using FSTW_backend.Dto;

namespace FSTW_backend.Services
{
    public interface IResumeEditorService
    {
        public Task CreateEmptyResume(int userId);

        public Task SendAboutInfo(int userId, AboutDto aboutDto);
    }
}
