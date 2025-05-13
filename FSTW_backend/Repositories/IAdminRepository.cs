using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IAdminRepository
    {
        public Task CreateInternship(Internship internship);

        public Task<Internship> GetUnarchiveInternship(int internshipId);

        public Task<Internship> GetInternship(int internshipId);

        public Task<List<Internship>> GetAllInternships(string filterParam);

        public Task SaveChangesAsync(int internshipId);

        public Task DeleteInternship(Internship internship);

        public Task ArchiveInternship(Internship internship);

        public Task UnarchiveInternship(Internship internship);
    }
}
