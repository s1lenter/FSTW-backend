using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IIntershipRepository
    {
        public Task<List<Internship>> GetInternships(InternshipFiltersDto filters);
    }
}
