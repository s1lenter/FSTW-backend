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
            await _service.CreateEmptyResume(int.Parse(userId));
            return Ok();
        }

        //[HttpPost("education")]
        //public async Task<IActionResult> AddEducationInfo()
        //{

        //}

        private string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
        }
    }
}
