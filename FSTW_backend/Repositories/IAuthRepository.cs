using FSTW_backend.Dto;
using FSTW_backend.Models;
using System.Reflection.Metadata.Ecma335;

namespace FSTW_backend.Repositories
{
    public interface IAuthRepository
    {
        public User? GetUser(UserAuthDto user);

        public User? GetUser(int id);

        public void CreateUser(User userDto);

        public void SaveRefreshToken(RefreshTokenRequestDto refreshTokenRequestDto);

        public RefreshToken GetRefreshToken(string token);

        public User? GetUser(string userName);
    }
}
