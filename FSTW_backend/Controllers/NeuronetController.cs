﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using FSTW_backend.Services.ResumeService;
using FSTW_backend.Services.Neuro;
using System.Threading.Tasks;

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

        [HttpGet("default_chat/history/{count}/{page}")]
        public async Task<IActionResult> GetDefaultChatHistory([FromRoute] int count, [FromRoute] int page)
        {
            var response = await _service.GetDefaultChatHistory(GetUserId(), count, page);
            return Ok(response.Value);
        }

        [HttpGet("resume_chat/{resumeId}/history/{count}/{page}")]
        public async Task<IActionResult> GetResumeChatHistory([FromRoute] int resumeId, [FromRoute] int count, [FromRoute] int page)
        {
            var response = await _service.GetResumeChatHistory(GetUserId(), resumeId, count, page);
            return Ok(response.Value);
        }

        [HttpPost("resume_chat/send/{resumeId}")]
        public async Task<IActionResult> SendResumeQuestion([FromServices] IResumeEditorService resumeEditorService, [FromBody] string question, [FromRoute] int resumeId)
        {
            var resumeResponse = await resumeEditorService.GetOnlyResumeInfo(GetUserId(), resumeId);
            if (!resumeResponse.Successed)
                return BadRequest(resumeResponse.Errors);
            var response = await _service.GetResumeAnswer(GetUserId(), resumeId, resumeResponse.Value, question, _httpClient);
            if (!response.Successed)
                return BadRequest();
            return Ok(response.Value);
        }

        [HttpPost("default_chat/send")]
        public async Task<IActionResult> SendQuestion([FromBody] string question)
        {
            var response = await _service.GetDefaultAnswer(GetUserId(), question, _httpClient);
            if (!response.Successed)
                return BadRequest(response.Errors);
            return Ok(response.Value);
        }

        [HttpPost("fill_db/{count}")]
        public async Task<IActionResult> FillDataBase([FromBody] string text, [FromRoute] int count)
        {
            await _service.FillDb(text, count, GetUserId());
            return Ok();
        }

        //[HttpPost("requests")]
        //public async Task<IActionResult> SendRequests([FromBody] string text)
        //{
        //    var list = new List<Task<string>>();
        //    for (var i = 0; i < 10; i++)
        //    {
        //         list.Add(_service.SendRequests(text, _httpClient));
        //    }
        //    var results = await Task.WhenAll(list);
        //    return Ok(results);
        //}


        private int GetUserId()
        {
            return int.Parse(HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
        }
    }
}
