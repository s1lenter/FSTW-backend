using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateInternship(Internship internship)
        {
            await _context.Internship.AddAsync(internship);
            await _context.SaveChangesAsync();
        }

        public async Task<Internship> GetInternship(int internshipId)
        {
            var internship = await _context.Internship.FirstOrDefaultAsync(i => i.Id == internshipId);
            return internship;
        }

        public async Task<List<Internship>> GetAllInternships()
        {
            return await _context.Internship.ToListAsync();
        }
    }
}
