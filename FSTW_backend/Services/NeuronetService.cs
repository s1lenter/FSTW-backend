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
        public async Task<ResponseResult<string>> GetAnswer(string question, HttpClient client)
        {
            var aiService = new GptService(client);
            var prevMessages = await _repository.GetPrevMessages();
            var context = new List<Dictionary<string, string>>();

            foreach (var message in prevMessages)
            {
                context.Add(new Dictionary<string, string>()
                {
                    ["role"] = "user",
                    ["content"] = $"{message.Message}"
                });
                context.Add(new Dictionary<string, string>()
                {
                    ["role"] = "assistant",
                    ["content"] = $"{message.Answer}"
                });
            }

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "user",
                ["content"] = $"{question}"
            });

            var response = await aiService.SendRequest(question, context);
            await _repository.AddMessage(question, response);
            return ResponseResult<string>.Success(response);
        }
    }
}
