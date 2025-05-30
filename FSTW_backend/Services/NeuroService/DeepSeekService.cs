﻿using System.Text.Json;
using FSTW_backend.Models;

namespace FSTW_backend.Services.Neuro
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
        }

        public async Task<string> SendMessageAsync(string userMessage, List<ChatHistory> contextList)
        {

            var httpRequest = new HttpRequestMessage(new HttpMethod("POST"), _httpClient.BaseAddress);


            var content = $"{userMessage}";

            if (contextList.Count != 0)
            {
                content += ", предыдущий контекст вопросов был такой:";
                foreach (var context in contextList)
                {
                    content += $"{context.Message}";
                }
            }

            var json = JsonSerializer.Serialize(new
            {
                model = "microsoft/mai-ds-r1:free",
                messages = new[]
                {
                //new { role = "system", content = "Ты помощник с составлением резюме, есть определенная структура, тебе скинут текст резюме по блокам, ты должен оценить и сказать что можно улучшить"},
                new { role = "user", content }
            },
            });

            httpRequest.Content = new StringContent(json);

            var httpResponse = await _httpClient.SendAsync(httpRequest);
            var response = await httpResponse.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(response);
            var message = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content");
            return message.ToString();
        }
    }
}
