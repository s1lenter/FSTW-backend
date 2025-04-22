using FSTW_backend.Dto;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/register")]
        public IActionResult Register([FromForm] UserAuthDto userAuthDto)
        {
            var response = _authService.Register(userAuthDto);
            if (response is null)
                return BadRequest("Error");
            return Ok(response);
        }

        [HttpPost("/login")]
        public IActionResult Login([FromForm] UserAuthDto userAuthDto)
        {
            var response = _authService.Login(userAuthDto);
            if (response is null)
                return BadRequest("Error");
            return Ok("You are login!");
        }

        [Authorize]
        [HttpGet("/secret")]
        public IActionResult OnlyAuthorizeEndpoint()
        {
            return Ok("You authorized!");
        }
    }
}
