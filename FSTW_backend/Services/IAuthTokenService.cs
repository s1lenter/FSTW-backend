using FSTW_backend.Models;

namespace FSTW_backend.Services
{
    public interface IAuthTokenService
    {
        public string CreateToken(User user);

        public string CreateRefreshToken(User user);
    }
}
