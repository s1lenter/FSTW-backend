using FSTW_backend.Dto;
using FSTW_backend.Dto.AuthDto;
using FSTW_backend.Models;
using System.Reflection.Metadata.Ecma335;

namespace FSTW_backend.Repositories.Auth
{
    public interface IAuthRepository
    {
        public Task<User?> GetUserAync(UserRegisterRequestDto user);

        public Task<User?> GetUserAync(UserLoginDto user);

        public Task<User?> GetUserAsync(int id);

        public Task CreateUserAsync(User userDto);

        public Task SaveRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);

        public Task<RefreshToken> GetRefreshTokenAsync(string token);

        public Task<RefreshToken> GetRefreshTokenAsync(int userId);

        public Task<User?> GetUserByUsernameAsync(string userName);

        public Task<User?> GetUserByEmailAsync(string email);

        public Task DeleteRefreshTokenAsync(int userId);
    }
}
