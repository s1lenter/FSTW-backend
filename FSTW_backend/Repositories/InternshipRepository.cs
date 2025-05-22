using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories
{
    public class InternshipRepository : IIntershipRepository
    {
        private AppDbContext _context;
        public InternshipRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Internship>> GetInternships(InternshipFiltersDto filters)
        {
            var query = _context.Internship.AsQueryable().Where(i => !i.isArchive);

            if (filters.WorkFormat is not null)
                query = query.Where(i => i.WorkFormat == filters.WorkFormat);

            if (filters.Salary == "Да")
                query = query.Where(i => i.SalaryTo != 0 || i.SalaryFrom != 0);
            else if (filters.Salary == "Нет")
                query = query.Where(i => i.SalaryTo == 0 && i.SalaryFrom == 0);

            if (filters.Direction is not null)
                query = query.Where(i => i.Direction == filters.Direction);

            return await query.ToListAsync();
        }
    }
}
