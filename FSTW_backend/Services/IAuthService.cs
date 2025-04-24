using FSTW_backend.Dto;
using FSTW_backend.Models;
using System.Security.Principal;

namespace FSTW_backend.Services
{
    public interface IAuthService
    {
        public User? Register(UserAuthDto userDto);

        public TokenResponseDto? Login(UserAuthDto userDto, HttpContext httpContext);

        public string Logout(HttpContext context);

        public string? RefreshAccessToken(RefreshTokenRequestDto refreshTokenRequestDto, string accessToken, HttpContext httpContext);
    }
}
