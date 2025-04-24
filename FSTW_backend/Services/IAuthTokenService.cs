using FSTW_backend.Dto;
using FSTW_backend.Models;
using System.Security.Claims;

namespace FSTW_backend.Services
{
    public interface IAuthTokenService
    {
        public string CreateToken(User user);

        public Task<string> GenearateAndSaveRefreshTokenAsync(User user);

        public ClaimsPrincipal GetClaimsFromToken(string token);

        public Task<string> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto, User user);

        public string UpdateAccessToken(IEnumerable<Claim> claims);
    }
}
