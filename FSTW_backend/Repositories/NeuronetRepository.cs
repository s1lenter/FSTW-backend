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
        public async Task AddResumeMessage(string message, string answer)
        {
            await _context.ChatHistory.AddAsync(new ChatHistory() { Message = message , Answer = answer});
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

        public async Task<List<ChatHistory>> GetResumePrevMessages()
        {
            return await _context.ChatHistory.Take(10).ToListAsync();
        }

        public async Task<List<HelperChatHistory>> GetDefaultPrevMessages()
        {
            return await _context.HelperChatHistory.Take(10).ToListAsync();
        }
    }
}
