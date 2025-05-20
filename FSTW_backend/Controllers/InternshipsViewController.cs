using FSTW_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("internship/")]
    public class InternshipsViewController : ControllerBase
    {
        private IInternshipService _service;
        public InternshipsViewController(IInternshipService service)
        {
            _service = service;    
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllInternships()
        {
            var response = _service.GetAllInternships();
            return Ok(response.Value);
        }
    }
}
