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

        public void CreateUser(User user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }

        public void SaveRefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var existToken = _dbContext.RefreshToken.FirstOrDefault(t => t.UserId == refreshTokenRequestDto.UserId);
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
                _dbContext.RefreshToken.Add(refreshToken);
            }
            _dbContext.SaveChanges();
        }

        public User? GetUser(UserAuthDto userDto)
        {
            return _dbContext.User.FirstOrDefault(u => u.Login == userDto.Login);
        }

        public User? GetUser(int id)
        {
            return _dbContext.User.FirstOrDefault(u => u.Id == id);
        }

        public User? GetUser(string userName)
        {
            return _dbContext.User.FirstOrDefault(u => u.Login == userName);
        }

        public RefreshToken? GetRefreshToken(string token)
        {
            return _dbContext.RefreshToken.FirstOrDefault(t => t.Token == token);
        }
    }
}
