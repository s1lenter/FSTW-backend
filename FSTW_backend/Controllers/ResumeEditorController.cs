using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;
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
            var response = await _service.SendExperienceInfo(int.Parse(GetUserId()), resumeId, experienceDto);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("projects/{resumeId}")]
        public async Task<IActionResult> SendProjects([FromRoute] int resumeId,[FromBody] List<ProjectDto> projectDtos)
        {
            var response = await _service.SendProjects(int.Parse(GetUserId()), resumeId, projectDtos);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("achievements/{resumeId}")]
        public async Task<IActionResult> SendAchievements([FromRoute] int resumeId, [FromBody] List<AchievementDto> achievementDtos)
        {
            var response = await _service.SendAchievements(int.Parse(GetUserId()), resumeId, achievementDtos);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("skills/{resumeId}")]
        public async Task<IActionResult> SendEducation([FromRoute] int resumeId, [FromBody] string skills)
        {
            var response = await _service.SendSkills(int.Parse(GetUserId()), resumeId, skills);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("education/{resumeId}")]
        public async Task<IActionResult> SendEducation([FromRoute] int resumeId, [FromBody] List<EducationDto> educationDtos)
        {
            var response = await _service.SendEducation(int.Parse(GetUserId()), resumeId, educationDtos);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        private string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
        }
    }
}
