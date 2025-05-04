using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/resume_editor")]
    public class ResumeEditorController : ControllerBase
    {
        private IResumeEditorService _service;
        public ResumeEditorController(IResumeEditorService service)
        {
            _service = service;
        }

        [HttpPost("init")]
        public async Task<IActionResult> CreateEmptyResume()
        {
            var userId = GetUserId();
            var resumeId = await _service.CreateEmptyResume(int.Parse(userId));
            return Ok(resumeId);
        }

        //[HttpPost("education")]
        //public async Task<IActionResult> AddEducationInfo()
        //{

        //}

        [HttpPost("about_and_hobbies/{resumeId}")]
        public async Task<IActionResult> SendAboutInfo([FromRoute] int resumeId, [FromBody] AboutDto aboutDto)
        {
            var response = await _service.SendAboutInfo(int.Parse(GetUserId()), resumeId, aboutDto);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("experience/{resumeId}")]
        public async Task<IActionResult> SendExperience([FromRoute] int resumeId, [FromBody] ExperienceDto experienceDto)
        {
            await _service.SendExperienceInfo(int.Parse(GetUserId()), resumeId, experienceDto);
            return Ok();
        }

        //[HttpPost("projects")]
        //public async Task<IActionResult> SendProjects([FromBody] List<ProjectDto> projectDto)
        //{

        //}

        private string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
        }
    }
}
