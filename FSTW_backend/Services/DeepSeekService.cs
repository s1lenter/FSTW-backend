using System.Text.Json;

namespace FSTW_backend.Services
{
    class DeepSeekService
    {
        private readonly HttpClient _httpClient;
        //private readonly RestClient _client;
        private readonly string _apiKey;

        private string Model = "deepseek/deepseek-r1";

        public DeepSeekService()
        {
            _apiKey = "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";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://openrouter.ai/api/v1/chat/completions");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            //_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            //_client = new RestClient("https://openrouter.ai/api/v1/chat/completions");
            //_client.AddDefaultHeader("Authorization", $"Bearer {_apiKey}");
            //_client.AddDefaultHeader("Content-Type", "application/json");
        }

        public async Task<string> SendMessageAsync(string userMessage)
        {

            var httpRequest = new HttpRequestMessage(new HttpMethod("POST"), _httpClient.BaseAddress);

            var json = JsonSerializer.Serialize(new
            {
                model = "deepseek/deepseek-r1",
                messages = new[]
                {
                    //new { role = "system", content = "Ты помощник с составлением резюме, есть определенная структура, тебе скинут текст резюме по блокам, ты должен оценить и сказать что можно улучшить"},
                    new { role = "user", content = userMessage }
                },
            });

            httpRequest.Content = new StringContent(json);

            var httpResponse = await _httpClient.SendAsync(httpRequest);
            var response = await httpResponse.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(response);
            var message = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content");
            return message.ToString();

            //var request = new RestRequest
            //{
            //    Method = Method.Post
            //};

            //request.AddJsonBody(new
            //{
            //    model = "deepseek/deepseek-r1",
            //    messages = new[]
            //    {
            //        //new { role = "system", content = "Ты помощник с составлением резюме, есть определенная структура, тебе скинут текст резюме по блокам, ты должен оценить и сказать что можно улучшить"},
            //        new { role = "user", content = userMessage }
            //    },
            //});

            //var response = await _client.ExecuteAsync<ChatResponse>(request);

            //if (!response.IsSuccessful)
            //{
            //    throw new Exception($"Error: {response.StatusCode} - {response.ErrorMessage}");
            //}

            //return response.Data?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response from DeepSeek.";
        }
    }
}
