using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories
{
    public class PersonalCabinetRepository : IPersonalCabinetRepository
    {
        private AppDbContext _dbContext;
        public PersonalCabinetRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Profile> GetAllInfo(int userId)
        {
            return await _dbContext.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
