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
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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

        public async Task<ResponseResult<User>> RegisterAsync(UserRegisterDto userDto)
        {
            if (userDto.Password != userDto.PasswordRepeat)
                return ResponseResult<User>.Failure("Пароли не совпадают");
            else if (await _repository.GetUserByUsernameAsync(userDto.Login) is not null)
                return ResponseResult<User>.Failure("Такой пользователь уже существует");
            else if (await _repository.GetUserByEmailAsync(userDto.Email) is not null)
                return ResponseResult<User>.Failure("Пользователь с такой почтой уже существует");

            var user = AuthUserMapper.Map(userDto);
            await _repository.CreateUserAsync(user);
            return ResponseResult<User>.Success(user);
        }

        public async Task<ResponseResult<TokenResponseDto?>> LoginAsync(UserLoginDto userLoginDto, HttpContext httpContext)
        {
            var user = await _repository.GetUserAync(userLoginDto);
            if (user is null)
                return ResponseResult<TokenResponseDto?>.Failure("Такого пользователя не существует");
            
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password) 
                == PasswordVerificationResult.Success)
            {
                var accessToken = _tokenService.CreateToken(user);
                httpContext.Response.Cookies.Append("token", accessToken);
                var refreshToken = await _tokenService.GenearateAndSaveRefreshTokenAsync(user);
                return ResponseResult<TokenResponseDto?>.Success(new TokenResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            return ResponseResult<TokenResponseDto?>.Failure("Неверный пароль");
        }

        public async Task<ResponseResult<string>> LogoutAsync(HttpContext context)
        {
            var user = await _repository.GetUserByUsernameAsync(context.User.Identity.Name);
            if (user == null)
                return ResponseResult<string>.Failure("");

            context.Response.Cookies.Delete("token");

            await _repository.DeleteRefreshTokenAsync(user.Id);
            return ResponseResult<string>.Success("");
        }

        public async Task<ResponseResult<string>> RefreshAccessTokenAsync(string refreshToken, int userId, string accessToken, HttpContext httpContext)
        {
            var principal = _tokenService.GetClaimsFromToken(accessToken);
            var savedRefreshToken = await _repository.GetRefreshTokenAsync(refreshToken);

            if (savedRefreshToken is null)
                return ResponseResult<string>.Failure("Неверный refresh токен");
            else if (savedRefreshToken.RefreshTokenExpiryTime < DateTime.UtcNow)
                return ResponseResult<string>.Failure("Истек срок действия refresh токена");

            var newAccessToken = _tokenService.UpdateAccessToken(principal.Claims);

            httpContext.Response.Cookies.Delete("token");
            httpContext.Response.Cookies.Append("token", newAccessToken);

            return ResponseResult<string>.Success(newAccessToken);
        }
    }
}
