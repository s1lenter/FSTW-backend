using FSTW_backend.Dto.AuthDto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FSTW_backend.Services
{
    public class AuthTokenService : IAuthTokenService
    {
        IConfiguration _configuration;
        IAuthRepository _repository;
        public AuthTokenService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _repository = new AuthRepository(appDbContext);
        }

        private string CreateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> GenearateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = CreateRefreshToken(user);
            await _repository.SaveRefreshTokenAsync(new RefreshTokenRequestDto() { RefreshToken = refreshToken, UserId = user.Id});
            return refreshToken;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            return CreateAccessToken(claims);
        }

        private string CreateAccessToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:TokenKey")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public string UpdateAccessToken(IEnumerable<Claim> claims)
        {
            return CreateAccessToken(claims.ToList());
        }

        public async Task<ClaimsPrincipal> GetClaimsFromToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:TokenKey")));
            TokenValidationParameters tokenParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["AppSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["AppSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:TokenKey"]!)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var result = await tokenHandler.ValidateTokenAsync(token, tokenParams);
            var principal = new ClaimsPrincipal(result.ClaimsIdentity);
            //var principal = tokenHandler.ValidateToken(token, tokenParams, out SecurityToken securityToken);
            var securityToken = result.SecurityToken;
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private async Task<RefreshToken?> ValidateTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var refreshToken = await _repository.GetRefreshTokenAsync(refreshTokenRequestDto.RefreshToken);
            if (refreshToken == null || refreshTokenRequestDto.RefreshToken != refreshToken.Token || 
                refreshToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;
            return refreshToken;
        }

        public async Task<string> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto, User user)
        {
            var validateRefreshToken = await ValidateTokenAsync(refreshTokenRequestDto);
            if (validateRefreshToken is null)
                return null;
            return CreateToken(user);
        }
    }
}
