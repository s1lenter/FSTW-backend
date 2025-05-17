using AutoMapper;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("neuronet")]
    public class NeuronetController : ControllerBase
    {
        private INeuronetService _service;
        private readonly HttpClient _httpClient;

        public NeuronetController(INeuronetService service, IHttpClientFactory httpClientFactory)
        {
            _service = service;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("/send")]
        public async Task<IActionResult> SendQuestion([FromBody] string question)
        {
            var response = await _service.GetAnswer(question, _httpClient);
            if (!response.Successed)
                return BadRequest();
            return Ok(response.Value);
        }

        //[HttpPost("py")]
        //public async Task<IActionResult> GetPy([FromBody] string message)
        //{
        //    var response = _service.GetAnswer(ques)
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return Ok(content);
        //    }

        //    return BadRequest();
        //}
    }
}
