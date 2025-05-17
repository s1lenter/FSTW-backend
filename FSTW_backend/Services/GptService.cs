using FSTW_backend.Models;
using System.Net.Http;

namespace FSTW_backend.Services
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
            //var systemPrev = $"{message}";

            //if (contextList.Count != 0)
            //{
            //    systemPrev += "предыдущий контекст вопросов был такой:";
            //    foreach (var context in contextList)
            //        systemPrev += $" {context.Message};";
            //}

            var jsonContent = JsonContent.Create(contextList);

            var response = await _httpClient.PostAsync("http://localhost:7000/api/hello", jsonContent);
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
