using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Services.PersonalCabinet
{
    public interface IPersonalCabinetService
    {
        public Task<PersonalCabinetDto> GetAllInfo(int userId);

        public Task<Profile> CreatePersonalInfo(int userId, PersonalCabinetDto personalCabinetDto);
    }
}
