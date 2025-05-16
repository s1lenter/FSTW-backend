using FSTW_backend.Repositories;

namespace FSTW_backend.Services
{
    public class NeuronetService : INeuronetService
    {
        private INeuronetRepository _repository;

        public NeuronetService(AppDbContext context)
        {
            _repository = new NeuronetRepository(context);
        }
        public async Task<ResponseResult<string>> GetAnswer(string question)
        {
            var deepSeekService = new DeepSeekService();
            var prevMes = await _repository.GetPrevMessages();
            var response = await deepSeekService.SendMessageAsync(question, prevMes);
            await _repository.AddMessage(question);
            return ResponseResult<string>.Success(response);
        }
    }
}
