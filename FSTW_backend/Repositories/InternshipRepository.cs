using FSTW_backend.Models;

namespace FSTW_backend.Repositories
{
    public class InternshipRepository : IIntershipRepository
    {
        private AppDbContext _context;
        public InternshipRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Internship> GetAllInternships()
        {
            return _context.Internship.Where(i => !i.isArchive).ToList();
        }

        public Task<Internship> GetInternshipById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
