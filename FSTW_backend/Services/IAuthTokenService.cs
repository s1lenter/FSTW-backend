using FSTW_backend.Dto;
using FSTW_backend.Models;
using System.Security.Claims;

namespace FSTW_backend.Services
{
    public interface IAuthTokenService
    {
        public string CreateToken(User user);

        public string GenearateAndSaveRefreshToken(User user);

        public ClaimsPrincipal GetClaimsFromToken(string token);

        public string RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto, User user);

        public string UpdateAccessToken(IEnumerable<Claim> claims);
    }
}
