using FSTW_backend.Dto;
using FSTW_backend.Mapping;
using FSTW_backend.Models;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/personal_cabinet")]
    public class PersonalCabinetController : ControllerBase
    {
        private IPersonalCabinetService _service;
        public PersonalCabinetController(IPersonalCabinetService service)
        {
            _service = service;
        }

        [HttpGet("all_info")]
        public async Task<IActionResult> GetAllInfo()
        {
            var userId = GetUserId();
            var response = await _service.GetAllInfo(int.Parse(userId));
            return Ok(response);
        }

        [HttpPost("send_info")]
        public async Task<IActionResult> CreatePersonalInfo([FromForm] PersonalCabinetDto personalCabinetDto)
        {
            var userId = GetUserId();
            await _service.CreatePersonalInfo(int.Parse(userId), personalCabinetDto);
            return Ok();
        }

        private string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(c => 
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
        }
    }
}
