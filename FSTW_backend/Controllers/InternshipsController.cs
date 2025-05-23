using FSTW_backend.Dto;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("internship/")]
    [Authorize]
    public class InternshipsController : ControllerBase
    {
        private IInternshipService _service;
        public InternshipsController(IInternshipService service)
        {
            _service = service;    
        }

        [HttpGet]
        public async Task<IActionResult> GetInternships([FromQuery] InternshipFiltersDto filters)
        {
            var response = await _service.GetInternships(filters);
            return Ok(response.Value);
        }

        [HttpGet("favorite")]
        public async Task<IActionResult> GetFavoriteInternships()
        {
            var response = await _service.GetFavoriteInternships(GetUserId());
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPost("add_favorite/{internshipId}")]
        public async Task<IActionResult> AddFavoriteInternship([FromRoute] int internshipId)
        {
            var response = await _service.AddFavoriteInternship(GetUserId(), internshipId);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpDelete("remove_favorite/{internshipId}")]
        public async Task<IActionResult> RemoveFavoriteInternship([FromRoute] int internshipId)
        {
            var response = await _service.RemoveFavoriteInternship(GetUserId(), internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        private int GetUserId()
        {
            return int.Parse(HttpContext.User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
        }
    }
}