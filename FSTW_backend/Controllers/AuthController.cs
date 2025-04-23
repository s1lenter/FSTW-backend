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
            var response = _authService.Login(userAuthDto, HttpContext);
            if (response is null)
                return BadRequest("Error");
            return Ok(response);
        }

        [HttpPost("/refresh")]
        public ActionResult<string> RefreshAccessToken([FromBody] RefreshTokenRequestDto refreshRequestTokenDto)
        {
            var newAccessToken = _authService.RefreshAccessToken(refreshRequestTokenDto, HttpContext.Request.Headers.Authorization.ToString().Split()[1]);
            if (newAccessToken is null)
                return BadRequest("Invalid refresh token");
            return newAccessToken;
        }

        [Authorize]
        [HttpGet("/secret")]
        public IActionResult OnlyAuthorizeEndpoint()
        {
            return Ok("You authorized!");
        }
    }
}
