namespace FSTW_backend.Services
{
    public class NeuronetService : INeuronetService
    {
        public async Task<ResponseResult<string>> GetAnswer(string question)
        {
            var deepSeekService = new DeepSeekService();
            var response = await deepSeekService.SendMessageAsync(question);
            return ResponseResult<string>.Success(response);
        }
    }
}
