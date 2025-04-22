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

        public User? GetUser(UserAuthDto userDto)
        {
            return _dbContext.User.FirstOrDefault(u => u.Login == userDto.Login);
        }
    }
}
