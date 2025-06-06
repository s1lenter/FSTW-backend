using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace FSTW_backend.Repositories.Admin
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
            var internship = await _context.Internship.FirstOrDefaultAsync(i => i.Id == internshipId);
            return internship;
        }

        public async Task<List<Internship>> GetAllInternships(string filterParam)
        {
            if (filterParam == "archive")
                return await _context.Internship.Where(i => i.isArchive).OrderBy(i => i.Id).ToListAsync();
            if (filterParam == "active")
                return await _context.Internship.Where(i => !i.isArchive).OrderBy(i => i.Id).ToListAsync();
            if (filterParam == "all")
                return await _context.Internship.OrderBy(i => i.Id).ToListAsync();
            return new List<Internship>();
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

        public async Task AddVacanciesFromHh(List<Internship> internships)
        {
            await _context.Internship.AddRangeAsync(internships);
            await _context.SaveChangesAsync();
        }

        public async Task<Internship> GetHhInternship(string intershipsId)
        {
            return await _context.Internship.FirstOrDefaultAsync(i => i.IdFromHh == intershipsId);
        }

        public async Task<List<Internship>> GetHhInterships()
        {
            return await _context.Internship.Where(i => i.Link.Substring(0, 14) == "https://hh.ru/").ToListAsync();
        }

        public async Task<List<Internship>> GetInternshipsByCompany(string name)
        {
            return await _context.Internship.Where(i => i.CompanyName == name && i.IdFromHh != null).ToListAsync();
        }

        public async Task DeleteOldInternships(List<Internship> internships)
        {
            _context.Internship.RemoveRange(internships);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
