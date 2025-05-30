using FSTW_backend.Dto.InternshipDto;
using FSTW_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Services.Internships
{
    public interface IInternshipService
    {
        public Task<ResponseResult<List<InternshipDto>>> GetInternships(InternshipFiltersDto filters);

        public Task<ResponseResult<List<InternshipDto>>> GetFavoriteInternships(int userId);

        public Task<ResponseResult<Favorite>> AddFavoriteInternship(int userId, int internshipId);

        public Task<ResponseResult<int>> RemoveFavoriteInternship(int userId, int internshipId);

        public Task<ResponseResult<List<InternshipDto>>> GetPersonalInterships(int userId);
    }
}
