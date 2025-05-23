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

        public async Task<List<Favorite>> GetFavorites(int userId)
        {
            return await _context.Favorite.Where(f => f.UserID == userId).ToListAsync();
        }

        public async Task<List<Internship>> GetFavoriteInternships(List<Favorite> favorites)
        {
            var result = new List<Internship>();

            foreach (var favorite in favorites)
                result.Add(await GetInternship(favorite.InternshipId));
            return result;
        }

        public async Task AddFavoriteInternship(Favorite favorite)
        {
            _context.Favorite.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<Favorite> GetFavoriteInternship(int userId, int internshipId)
        {
            var fav = await _context.Favorite.FirstOrDefaultAsync(f => 
                f.InternshipId == internshipId && f.UserID == userId);
            return fav;
        }

        public async Task<int> DeleteFavoriteInternship(Favorite favorite)
        {
            _context.Favorite.Remove(favorite);
            await _context.SaveChangesAsync();
            return favorite.Id;
        }

        public async Task<Internship> GetInternship(int internshipId)
        {
            return await _context.Internship.FirstOrDefaultAsync(i => i.Id == internshipId);
        }

    }
}
