using FSTW_backend.Dto.AuthDto;
using FSTW_backend.Models;
using System.Security.Claims;

namespace FSTW_backend.Services.Auth
{
    public interface IAuthTokenService
    {
        public string CreateToken(User user);

        public Task<string> GenearateAndSaveRefreshTokenAsync(User user);

        public Task<ClaimsPrincipal> GetClaimsFromToken(string token);

        public Task<string> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto, User user);

        public string UpdateAccessToken(IEnumerable<Claim> claims);
    }
}
