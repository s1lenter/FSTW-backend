using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        AppDbContext _dbContext;
        public AuthRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Profile.AddAsync(new Profile
            {
                User = user,
                UserId = user.Id
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var existToken = await _dbContext.RefreshToken.FirstOrDefaultAsync(t => t.UserId == refreshTokenRequestDto.UserId);
            if (existToken is not null)
            {
                existToken.Token = refreshTokenRequestDto.RefreshToken;
                existToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(10);
            }
            else
            {
                var refreshToken = new RefreshToken()
                {
                    Token = refreshTokenRequestDto.RefreshToken,
                    UserId = refreshTokenRequestDto.UserId,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(10)
                };
                await _dbContext.RefreshToken.AddAsync(refreshToken);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserAync(UserRegisterRequestDto userDto)
        {
            return await GetUserByUsernameAsync(userDto.Login);
        }

        public async Task<User?> GetUserAync(UserLoginDto userDto)
        {
            return await GetUserByUsernameAsync(userDto.Login);
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByUsernameAsync(string userName)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Login == userName);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _dbContext.RefreshToken.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task DeleteRefreshTokenAsync(int userId)
        {
            var token = await _dbContext.RefreshToken.FirstOrDefaultAsync(t => t.UserId == userId);
            if (token is not null)
                _dbContext.RefreshToken.Remove(token);
        }
    }
}
