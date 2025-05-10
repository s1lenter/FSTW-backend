using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Mapping;
using Microsoft.EntityFrameworkCore;
using Profile = FSTW_backend.Models.Profile;

namespace FSTW_backend.Repositories
{
    public class PersonalCabinetRepository : IPersonalCabinetRepository
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        public PersonalCabinetRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Profile> CreatePersonalCabinetInfo(int userId, PersonalCabinetDto personalCabinetDto)
        {
            var profile = await GetAllInfoAsync(userId);
            _mapper.Map(personalCabinetDto, profile);

            profile.DateOfBirth = profile.DateOfBirth.ToUniversalTime();
            await _dbContext.SaveChangesAsync();
            return profile;
        }

        public async Task<Profile> GetAllInfoAsync(int userId)
        {
            return await _dbContext.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
