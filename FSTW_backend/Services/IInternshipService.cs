using FSTW_backend.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Services
{
    public interface IInternshipService
    {
        public Task<ResponseResult<List<InternshipDto>>> GetInternships(InternshipFiltersDto filters);
    }
}
