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

        [HttpPost("create/multiply")]
        public async Task<IActionResult> CreateInternship([FromBody] List<InternshipDto> internshipDtos)
        {
            foreach (var internshipDto in internshipDtos)
            {
                var response = await _service.CreateInternship(internshipDto);
                if (!response.Successed)
                    return BadRequest(response.Errors);
            }
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInternship([FromBody] InternshipDto internshipDto)
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
        public async Task<IActionResult> GetInternship([FromQuery] string filterParam)
        {
            var response = await _service.GetAllInternships(filterParam);
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

        [HttpDelete("internship/{internshipId}/remove")]
        public async Task<IActionResult> DeleteInternship([FromRoute] int internshipId)
        {
            var response = await _service.DeleteInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPut("internship/{internshipId}/archive")]
        public async Task<IActionResult> ArchiveInternship([FromRoute] int internshipId)
        {
            var response = await _service.ArchiveInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPut("internship/{internshipId}/unarchive")]
        public async Task<IActionResult> UnarchiveInternship([FromRoute] int internshipId)
        {
            var response = await _service.UnarchiveInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }
    }
}
