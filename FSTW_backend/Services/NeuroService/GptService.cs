using FSTW_backend.Models;
using System.Net.Http;
using System.Text.Json;
using FSTW_backend.Dto;

namespace FSTW_backend.Services.Neuro
{
    public class GptService
    {
        private HttpClient _httpClient;
        public GptService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<string> SendRequest(string message, List<Dictionary<string, string>> contextList)
        {
            var jsonContent = JsonContent.Create(contextList);

            var response = await _httpClient.PostAsync("http://neuro:5000/api/hello", jsonContent);
            //var response = await _httpClient.PostAsync("http://192.168.0.104:5000/api/hello", jsonContent);
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> SendRequestDS(string message, List<Dictionary<string, string>> contextList)
        {
            var _apiKey = "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";
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://openrouter.ai/api/v1/chat/completions");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");


            var httpRequest = new HttpRequestMessage(new HttpMethod("POST"), _httpClient.BaseAddress);

            var json = JsonSerializer.Serialize(new
            {
                model = "microsoft/mai-ds-r1:free",
                messages = contextList
            });

            httpRequest.Content = new StringContent(json);


            var httpResponse = await client.SendAsync(httpRequest);
            var response = await httpResponse.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(response);
            var resp = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content");
            return resp.ToString();



            //var jsonContent = JsonContent.Create(contextList);

            //var response = await _httpClient.PostAsync("http://neuro:5000/api/hello", jsonContent);
            ////var response = await _httpClient.PostAsync("http://192.168.0.104:5000/api/hello", jsonContent);
            //if (!response.IsSuccessStatusCode)
            //    return null;
            //var content = await response.Content.ReadAsStringAsync();
            //return content;
        }
    }
}
