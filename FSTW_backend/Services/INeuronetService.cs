namespace FSTW_backend.Services
{
    public interface INeuronetService
    {
        public Task<ResponseResult<string>> GetAnswer(string question);
    }
}
