using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Services
{
    public interface IPersonalCabinetService
    {
        public Task<Profile> GetAllInfo(int userId);
    }
}
