using FSTW_backend.Dto;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("internship/")]
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
    }
}