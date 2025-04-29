using FSTW_backend.Dto;
using FSTW_backend.Mapping;
using FSTW_backend.Models;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("api/personal_cabinet")]
    public class PersonalCabinetController : ControllerBase
    {
        private IPersonalCabinetService _service;
        public PersonalCabinetController(IPersonalCabinetService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("all_info")]
        public async Task<IActionResult> GetAllInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
            var response = await _service.GetAllInfo(int.Parse(userId));
            var x = AuthUserMapper.MapProfile<Profile, PersonalCabinetDto>(response);
            return Ok(x);
        }
    }
}
