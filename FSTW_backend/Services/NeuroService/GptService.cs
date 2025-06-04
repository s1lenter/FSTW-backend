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

            //var response = await _httpClient.PostAsync("http://neuro:5000/api/hello", jsonContent);
            var response = await _httpClient.PostAsync("http://10.13.79.123:5000/api/hello", jsonContent);
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
