using FSTW_backend.Dto;
using FSTW_backend.Models;
using System.Reflection.Metadata.Ecma335;

namespace FSTW_backend.Repositories
{
    public interface IAuthRepository
    {
        public Task<User?> GetUserAync(UserRegisterDto user);

        public Task<User?> GetUserAync(UserLoginDto user);

        public Task<User?> GetUserAsync(int id);

        public Task CreateUserAsync(User userDto);

        public Task SaveRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);

        public Task<RefreshToken> GetRefreshTokenAsync(string token);

        public Task<User?> GetUserByUsernameAsync(string userName);

        public Task<User?> GetUserByEmailAsync(string email);

        public Task DeleteRefreshTokenAsync(int userId);
    }
}
