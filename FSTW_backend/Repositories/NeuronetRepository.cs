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
        public async Task AddMessage(string message, string answer)
        {
            await _context.ChatHistory.AddAsync(new ChatHistory() { Message = message , Answer = answer});
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatHistory>> GetPrevMessages()
        {
            return await _context.ChatHistory.Take(10).ToListAsync();
        }
    }
}
