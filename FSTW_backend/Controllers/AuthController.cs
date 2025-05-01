using FSTW_backend.Dto;
using FSTW_backend.Filters;
using FSTW_backend.Models;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

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
        public async Task<IActionResult> Register([FromForm] UserRegisterRequestDto userAuthDto)
        {
            var response = await _authService.RegisterAsync(userAuthDto);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync([FromForm] UserLoginDto userLoginDto)
        {
            var response = await _authService.LoginAsync(userLoginDto, HttpContext);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);

        }

        [Authorize]
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.LogoutAsync(HttpContext);
            if (response is not null)
                return Ok();
            return BadRequest("Error!");
        }

        [Authorize]
        [HttpPost("/refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] string refreshToken)
        {

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
            var response = await _authService.RefreshAccessTokenAsync(refreshToken, int.Parse(userId),
                HttpContext.Request.Headers.Authorization.ToString().Split()[1], HttpContext);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [Authorize]
        [HttpGet("/secret")]
        public IActionResult OnlyAuthorizeEndpoint()
        {
            return Ok("You authorized!");
        }

        [HttpGet("/all")]
        public IActionResult Endpoint()
        {
            var user = new[]
            {
                new { Name = "aaaa" },
                new { Name = "bbbbb" }
            };
            return Ok(user);
        }

        [HttpPost("/all_post")]
        public IActionResult EndpointPost([FromForm] UserRegisterRequestDto text)
        {
            return Ok(text);
        }
    }
}
