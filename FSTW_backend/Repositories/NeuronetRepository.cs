using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkQueryableExtensions = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace FSTW_backend.Repositories
{
    public class NeuronetRepository : INeuronetRepository
    {
        private AppDbContext _context;
        public NeuronetRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddResumeMessage(int userId, int resumeId, string message, string answer)
        {
            await _context.ChatHistory.AddAsync(new ChatHistory()
            {
                Message = message, 
                Answer = answer,
                ResumeId = resumeId,
                UserId = userId
            });
            await _context.SaveChangesAsync();
        }

        public async Task AddDefaultMessage(string message, string answer, int userId)
        {
            await _context.HelperChatHistory.AddAsync(new HelperChatHistory()
            {
                Answer = answer,
                Message = message,
                UserId = userId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatHistory>> GetResumePrevMessages(int userId, int resumeId)
        {
            return await _context.ChatHistory.Where(h => h.UserId == userId && h.ResumeId == resumeId).Take(10).ToListAsync();
        }

        public async Task<List<HelperChatHistory>> GetDefaultPrevMessages(int userId)
        {
            return await _context.HelperChatHistory.Where(h => h.UserId == userId).Take(10).ToListAsync();
        }
    }
}
