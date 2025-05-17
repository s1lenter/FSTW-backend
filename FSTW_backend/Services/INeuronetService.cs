using FSTW_backend.Dto.ResumeDto;

namespace FSTW_backend.Services
{
    public interface INeuronetService
    {
        public Task<ResponseResult<string>> GetAnswer(OnlyResumeInfoDto resumeInfoDto, string question, HttpClient client);
    }
}
