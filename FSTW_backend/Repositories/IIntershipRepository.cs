using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public interface IIntershipRepository
    {
        public List<Internship> GetAllInternships();
        public Task<Internship> GetInternshipById(int id);
    }
}
