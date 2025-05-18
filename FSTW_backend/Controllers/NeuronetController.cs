using AutoMapper;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("neuronet")]
    [Authorize]
    public class NeuronetController : ControllerBase
    {
        private INeuronetService _service;
        private readonly HttpClient _httpClient;

        public NeuronetController(INeuronetService service, IHttpClientFactory httpClientFactory)
        {
            _service = service;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("/send/{resumeId}")]
        public async Task<IActionResult> SendQuestion([FromServices] IResumeEditorService resumeEditorService, [FromBody] string question, [FromRoute] int resumeId)
        {
            var resumeResponse = await resumeEditorService.GetOnlyResumeInfo(GetUserId(), resumeId);
            if (!resumeResponse.Successed)
                return BadRequest(resumeResponse.Errors);
            var response = await _service.GetResumeAnswer(resumeResponse.Value, question, _httpClient);
            if (!response.Successed)
                return BadRequest();
            return Ok(response.Value);
        }

        [HttpPost("/default_chat")]
        public async Task<IActionResult> SendQuestion([FromBody] string question)
        {
            var response = await _service.GetDefaultAnswer(GetUserId(), question, _httpClient);
            if (!response.Successed)
                return BadRequest(response.Errors);
            return Ok(response.Value);
        }

        private int GetUserId()
        {
            return int.Parse(HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
        }
    }
}
