using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories.PersonalCabinet
{
    public interface IPersonalCabinetRepository
    {
        public Task<Profile> GetAllInfoAsync(int userId);

        public Task<Profile> CreatePersonalCabinetInfo(int userId, PersonalCabinetDto personalCabinetDto);
    }
}
