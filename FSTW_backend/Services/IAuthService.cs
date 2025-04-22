using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Services
{
    public interface IAuthService
    {
        public User Register(UserAuthDto userDto);

        public User Login(UserAuthDto userDto);

        public string CreateToken(User user);
    }
}
