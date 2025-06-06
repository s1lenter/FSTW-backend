using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories.SiteParsingRep
{
    public class SitesParsingRepositry : ISitesParsingRepository
    {
        private AppDbContext _context;

        public SitesParsingRepositry(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Internship> GetInternshipWithLocalId(string id)
        {
            return await _context.Internship.FirstOrDefaultAsync(i => i.IdFromHh == id);
        }

        public async Task<List<Internship>> GetAutoInterships(string companyName)
        {
            return await _context.Internship.Where(i => i.CompanyName == companyName && i.IdFromHh != null).ToListAsync();
        }
    }
}
