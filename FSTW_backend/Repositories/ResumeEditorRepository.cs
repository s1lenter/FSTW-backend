
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories
{
    public class ResumeEditorRepository : IResumeEditorRepository
    {
        private AppDbContext _context;
        public ResumeEditorRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task CreateEmptyResume(int userId)
        {
            var profile = await GetUserProfileAsync(userId);
            var resume = new Resume()
            {
                UserId = userId,
                ProfileId = profile.Id,
            };
            await _context.Resume.AddAsync(resume);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeGoals(int userId, string goalsText)
        {
            var profile = await GetUserProfileAsync(userId);
            var resume = await GetResume(profile.Id);
            resume.Goal = goalsText;
            await _context.SaveChangesAsync();
        }

        private async Task<Resume> GetResume(int profileId)
        {
            return await _context.Resume.FirstOrDefaultAsync(r => r.ProfileId == profileId);
        }

        private async Task<User> GetUserAsync(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        private async Task<Profile> GetUserProfileAsync(int userId)
        {
            return await _context.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
