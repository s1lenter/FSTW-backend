using FSTW_backend.Dto;
using FSTW_backend.Dto.AuthDto;
using FSTW_backend.Models;
using System.Security.Principal;

namespace FSTW_backend.Services.Auth
{
    public interface IAuthService
    {
        public Task<ResponseResult<User>> RegisterAsync(UserRegisterRequestDto userDto);

        public Task<ResponseResult<TokenResponseDto?>> LoginAsync(UserLoginDto userLoginDto, HttpContext httpContext);

        public Task<ResponseResult<string>> LogoutAsync(HttpContext context);

        public Task<ResponseResult<string>> RefreshAccessTokenAsync(string accessToken, HttpContext httpContext);
    }
}
