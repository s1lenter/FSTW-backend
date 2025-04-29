using FSTW_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    //[ApiController]
    //[Route("api/personal_cabinet")]
    public class PersonalCabinetController : ControllerBase
    {
        private IPersonalCabinetService _service;
        public PersonalCabinetController(IPersonalCabinetService service)
        {
            _service = service;
        }
        public async Task<IActionResult> GetAllInfo()
        {
            var response = await _service.GetAllInfo(1);
            return Ok(response);
        }
    }
}
