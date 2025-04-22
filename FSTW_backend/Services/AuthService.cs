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

namespace FSTW_backend.Services
{
    public class AuthService : IAuthService
    {
        IAuthRepository _repository;
        IConfiguration _configuration;
        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _repository = new AuthRepository(appDbContext);
            _configuration = configuration;
        }

        public User? Register(UserAuthDto userDto)
        {
            if (userDto.Password != userDto.PasswordRepeat || _repository.GetUser(userDto) is not null)
                return null;

            var user = AuthUserMapper.Map(userDto);
            _repository.CreateUser(user);
            return user;
        }

        public User? Login(UserAuthDto userDto)
        {
            var user = _repository.GetUser(userDto);
            if (user is null)
                return null;
            
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userDto.Password) 
                == PasswordVerificationResult.Success)
            {
                var token = CreateToken(user);
                return user;
            }

            return null;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:TokenKey")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
