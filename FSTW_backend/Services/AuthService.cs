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
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FSTW_backend.Services
{
    public class AuthService : IAuthService
    {
        private IMapper _mapper;
        IAuthRepository _repository;
        IConfiguration _configuration;
        IAuthTokenService _tokenService;
        public AuthService(AppDbContext appDbContext, IConfiguration configuration, IAuthTokenService tokenService, IMapper mapper)
        {
            _repository = new AuthRepository(appDbContext);
            _configuration = configuration;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ResponseResult<User>> RegisterAsync(UserRegisterRequestDto userDto)
        {
            var errorRes = new List<Dictionary<string, string>>();
            if (userDto.Password != userDto.PasswordRepeat)
            {
                errorRes.Add(new (){["PasswordMatch"] = "Пароли не совпадают"});
                return ResponseResult<User>.Failure(errorRes);
            }
            if (await _repository.GetUserByUsernameAsync(userDto.Login) is not null)
            {
                errorRes.Add(new() { ["LoginError"] = "Такой пользователь уже существует" });
                return ResponseResult<User>.Failure(errorRes);
            }
            if (await _repository.GetUserByEmailAsync(userDto.Email) is not null)
            {
                errorRes.Add(new() { ["EmailError"] = "Пользователь с такой почтой уже существует" });
                return ResponseResult<User>.Failure(errorRes);
            }

            var user = new User();
            _mapper.Map(userDto, user);
            await _repository.CreateUserAsync(user);
            return ResponseResult<User>.Success(user);
        }

        public async Task<ResponseResult<TokenResponseDto?>> LoginAsync(UserLoginDto userLoginDto, HttpContext httpContext)
        {
            var errorRes = new List<Dictionary<string, string>>();

            var user = await _repository.GetUserAync(userLoginDto);
            if (user is null)
            {
                errorRes.Add(new() { ["LoginError"] = "Такой пользователь уже существует" });
                return ResponseResult<TokenResponseDto?>.Failure(errorRes);
            }

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
            errorRes.Add(new() { ["PasswordError"] = "Неверный пароль" });
            return ResponseResult<TokenResponseDto?>.Failure(errorRes);
        }

        public async Task<ResponseResult<string>> LogoutAsync(HttpContext context)
        {
            var errorRes = new List<Dictionary<string, string>>();

            var user = await _repository.GetUserByUsernameAsync(context.User.Identity.Name);
            if (user == null)
            {
                errorRes.Add(new() { [""] = "" });
                return ResponseResult<string>.Failure(errorRes);
            }

            context.Response.Cookies.Delete("token");

            await _repository.DeleteRefreshTokenAsync(user.Id);
            return ResponseResult<string>.Success("");
        }

        public async Task<ResponseResult<string>> RefreshAccessTokenAsync(string refreshToken, int userId, string accessToken, HttpContext httpContext)
        {
            var errorRes = new List<Dictionary<string, string>>();

            var principal = _tokenService.GetClaimsFromToken(accessToken);
            var savedRefreshToken = await _repository.GetRefreshTokenAsync(refreshToken);

            if (savedRefreshToken is null)
            {
                errorRes.Add(new() { ["LoginError"] = "Неверный refresh токен" });
                return ResponseResult<string>.Failure(errorRes);
            }
            if (savedRefreshToken.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                errorRes.Add(new() { ["LoginError"] = "Истек срок дейтсвия refresh токена" });
                return ResponseResult<string>.Failure(errorRes);
            }

            var newAccessToken = _tokenService.UpdateAccessToken(principal.Claims);

            httpContext.Response.Cookies.Delete("token");
            httpContext.Response.Cookies.Append("token", newAccessToken);

            return ResponseResult<string>.Success(newAccessToken);
        }
    }
}
