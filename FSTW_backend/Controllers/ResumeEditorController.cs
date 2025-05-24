using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FSTW_backend.Dto;
using FSTW_backend.Pdf;
using FSTW_backend.Services.ResumeService;

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
            var resumeId = await _service.CreateEmptyResume(GetUserId());
            return Ok(resumeId);
        }

        [HttpGet("all_user_resumes")]
        public async Task<IActionResult> GetAllResumes()
        {
            var userId = GetUserId();
            var response = _service.GetAllUserResumes(userId);

            return Ok(response.Value);
        }

        [HttpPost("about_and_hobbies/{resumeId}")]
        public async Task<IActionResult> SendAboutInfo([FromRoute] int resumeId, [FromBody] AboutDto aboutDto)
        {
            var response = await _service.SendAboutInfo(GetUserId(), resumeId, aboutDto);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("experience/{resumeId}")]
        public async Task<IActionResult> SendExperience([FromRoute] int resumeId, [FromBody] string experience)
        {
            var response = await _service.SendExperienceInfo(GetUserId(), resumeId, experience);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("projects/{resumeId}")]
        public async Task<IActionResult> SendProjects([FromRoute] int resumeId, [FromBody] List<ProjectDto> projectDtos)
        {
            var response = await _service.SendProjects(GetUserId(), resumeId, projectDtos);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("achievements/{resumeId}")]
        public async Task<IActionResult> SendAchievements([FromRoute] int resumeId, [FromBody] List<AchievementDto> achievementDtos)
        {
            var response = await _service.SendAchievements(GetUserId(), resumeId, achievementDtos);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("skills/{resumeId}")]
        public async Task<IActionResult> SendEducation([FromRoute] int resumeId, [FromBody] string skills)
        {
            var response = await _service.SendSkills(GetUserId(), resumeId, skills);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("education/{resumeId}")]
        public async Task<IActionResult> SendEducation([FromRoute] int resumeId, [FromBody] List<EducationDto> educationDtos)
        {
            var response = await _service.SendEducation(GetUserId(), resumeId, educationDtos);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpGet("all_info/{resumeId}")]
        public async Task<IActionResult> GetAllInfo([FromRoute] int resumeId)
        {
            var response = await _service.GetAllResumeInfo(GetUserId(), resumeId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpGet("only_resume_info/{resumeId}")]
        public async Task<IActionResult> GetOnlyResumeInfo([FromRoute] int resumeId)
        {
            var response = await _service.GetOnlyResumeInfo(GetUserId(), resumeId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpDelete("remove/{resumeId}")]
        public async Task<IActionResult> DeleteResume([FromRoute] int resumeId)
        {
            var response = await _service.DeleteResume(GetUserId(), resumeId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        private int GetUserId()
        {
            return int.Parse(HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
        }

        [HttpGet("downland/{resumeId}")]
        public async Task<IActionResult> DownlandFile([FromRoute] int resumeId)
        {
            var resumeInfoResponse = await _service.GetAllResumeInfo(GetUserId(), resumeId);
            byte[] pdfBytes = PdfCreator.CreatePdf(resumeInfoResponse.Value);
            return File(pdfBytes, "application/pdf", "resume.pdf");
        }

        [HttpPost("change_resume/{resumeId}")]
        public async Task<IActionResult> ChangeResumeInfo([FromBody] OnlyResumeInfoDto onlyResumeInfoDto, [FromRoute] int resumeId)
        {
            var response = await _service.ChangeResumeInfo(GetUserId(), resumeId, onlyResumeInfoDto);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }
    }
}
