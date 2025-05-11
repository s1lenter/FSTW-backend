using FSTW_backend.Dto;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("/admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminPanelController : ControllerBase
    {
        private IAdminService _service;
        public AdminPanelController(IAdminService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInternship([FromForm] InternshipDto internshipDto)
        {
            var response = await _service.CreateInternship(internshipDto);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpGet("internship/{internshipId}")]
        public async Task<IActionResult> GetInternship([FromRoute] int internshipId)
        {
            var response = await _service.GetInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpGet("all_internships")]
        public async Task<IActionResult> GetInternship()
        {
            var response = await _service.GetAllInternships();
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPost("edit_internship/{internshipId}")]
        public async Task<IActionResult> EditInternship([FromRoute] int internshipId, [FromBody] InternshipDto internshipDto)
        {
            var response = await _service.EditInternship(internshipId, internshipDto);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }
    }
}
