using FSTW_backend.Dto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;
using FSTW_backend.Mapping;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace FSTW_backend.Services
{
    public class AuthService : IAuthService
    {
        IAuthRepository _repository;
        IConfiguration _configuration;
        IAuthTokenService _tokenService;
        public AuthService(AppDbContext appDbContext, IConfiguration configuration, IAuthTokenService tokenService)
        {
            _repository = new AuthRepository(appDbContext);
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public User? Register(UserAuthDto userDto)
        {
            if (userDto.Password != userDto.PasswordRepeat || _repository.GetUser(userDto) is not null)
                return null;

            var user = AuthUserMapper.Map(userDto);
            _repository.CreateUser(user);
            return user;
        }

        public TokenResponseDto? Login(UserAuthDto userDto, HttpContext httpContext)
        {
            var user = _repository.GetUser(userDto);
            if (user is null)
                return null;
            
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userDto.Password) 
                == PasswordVerificationResult.Success)
            {
                var accessToken = _tokenService.CreateToken(user);
                httpContext.Response.Cookies.Append("token", accessToken);
                var refreshToken = _tokenService.GenearateAndSaveRefreshToken(user);
                return new TokenResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }

            return null;
        }

        public string? RefreshAccessToken(RefreshTokenRequestDto refreshTokenRequestDto, string accessToken)
        {
            if (_repository.GetUser(refreshTokenRequestDto.UserId) is null)
                return null;

            var principal = _tokenService.GetClaimsFromToken(accessToken);
            var refreshToken = _repository.GetRefreshToken(refreshTokenRequestDto.RefreshToken);

            if (refreshToken == null || refreshToken.RefreshTokenExpiryTime < DateTime.UtcNow)
                return null;

            var newAccessToken = _tokenService.UpdateAccessToken(principal.Claims);

            return newAccessToken;
        }
    }
}
