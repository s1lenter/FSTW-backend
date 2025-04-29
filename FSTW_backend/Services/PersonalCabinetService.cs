using FSTW_backend.Dto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;

namespace FSTW_backend.Services
{
    public class PersonalCabinetService : IPersonalCabinetService
    {
        private IPersonalCabinetRepository _repository;
        public PersonalCabinetService(AppDbContext appDbContext)
        {
            _repository = new PersonalCabinetRepository(appDbContext);
        }
        public async Task<Profile> GetAllInfo(int userId)
        {
            return await _repository.GetAllInfo(userId);
        }
    }
}
