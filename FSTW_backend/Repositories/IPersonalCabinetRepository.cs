using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IPersonalCabinetRepository
    {
        public Task<Profile> GetAllInfo(int userId);
    }
}
