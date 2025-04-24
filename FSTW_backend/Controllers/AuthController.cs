using FSTW_backend.Dto;
using FSTW_backend.Models;
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
        public async Task<IActionResult> Register([FromForm] UserRegisterDto userAuthDto)
        {
            var response = await _authService.RegisterAsync(userAuthDto);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Error);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync([FromForm] UserLoginDto UserLoginDto)
        {
            var response = await _authService.LoginAsync(UserLoginDto, HttpContext);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Error);

        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.LogoutAsync(HttpContext);
            if (response is not null)
                return Ok();
            return BadRequest("Error!");
        }

        [HttpPost("/refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] string refreshToken)
        {
            var userClaims = HttpContext.User.Claims.ToList();
            if (userClaims.Count == 0)
                return Unauthorized();

            var userId = userClaims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
            var response = await _authService.RefreshAccessTokenAsync(refreshToken, int.Parse(userId),
                HttpContext.Request.Headers.Authorization.ToString().Split()[1], HttpContext);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Error);
        }

        [Authorize]
        [HttpGet("/secret")]
        public IActionResult OnlyAuthorizeEndpoint()
        {
            return Ok("You authorized!");
        }
    }
}
