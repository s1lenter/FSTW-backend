using FSTW_backend.Dto.ResumeDto;

namespace FSTW_backend.Services
{
    public interface INeuronetService
    {
        public Task<ResponseResult<string>> GetResumeAnswer(int userId, int resumeId, OnlyResumeInfoDto resumeInfoDto, string question, HttpClient client);

        public Task<ResponseResult<string>> GetDefaultAnswer(int userId, string question, HttpClient client);
    }
}
