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
        public async Task AddMessage(string message)
        {
            await _context.ChatHistory.AddAsync(new ChatHistory() { Message = message });
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatHistory>> GetPrevMessages()
        {
            var result = new List<ChatHistory>();
            for (int i = 0; i < 5; i++)
            {
                var mes = await _context.ChatHistory.FirstOrDefaultAsync();
                if (mes == null)
                    break;
                result.Add(mes);
            }
            return result;
        }
    }
}
