using FSTW_backend.Dto;

namespace FSTW_backend.Services
{
    public interface IInternshipService
    {
        public ResponseResult<List<InternshipDto>> GetAllInternships();
        public Task<ResponseResult<InternshipDto>> GetInternshipById(string id);
    }
}
