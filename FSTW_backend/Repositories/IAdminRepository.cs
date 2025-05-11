using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IAdminRepository
    {
        public Task CreateInternship(Internship internship);

        public Task<Internship> GetInternship(int internshipId);

        public Task<List<Internship>> GetAllInternships();

        public Task SaveChangesAsync(int internshipId);
    }
}
