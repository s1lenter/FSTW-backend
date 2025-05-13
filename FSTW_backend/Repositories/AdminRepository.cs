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

        public async Task<Internship> GetUnarchiveInternship(int internshipId)
        {
            var internship = await _context.Internship.FirstOrDefaultAsync(i => i.Id == internshipId && !i.isArchive);
            return internship;
        }

        public async Task<Internship> GetInternship(int internshipId)
        {
            var internship = await _context.Internship.FirstOrDefaultAsync();
            return internship;
        }

        public async Task<List<Internship>> GetAllInternships()
        {
            return await _context.Internship.Where(i => !i.isArchive).ToListAsync();
        }

        public async Task SaveChangesAsync(int internshipId)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInternship(Internship internship)
        {
            _context.Internship.Remove(internship);
            await _context.SaveChangesAsync();
        }

        public async Task ArchiveInternship(Internship internship)
        {
            internship.isArchive = true;
            await _context.SaveChangesAsync();
        }

        public async Task UnarchiveInternship(Internship internship)
        {
            internship.isArchive = false;
            await _context.SaveChangesAsync();
        }
    }
}
