using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkQueryableExtensions = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace FSTW_backend.Repositories.Neuro
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

        public async Task<List<NeuronetDto>> GetMessagesDefaultHistory(int userId, int count, int page)
        {
            var x = await _context.HelperChatHistory
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.Id)
                .Skip(count * (page - 1))
                .Take(count)
                .ToListAsync();
            var dtosList = new List<NeuronetDto>();

            foreach (var duo in x)
            {
                var dto = new NeuronetDto()
                {
                    UserMessage = duo.Message,
                    BotMessage = duo.Answer
                };
                dtosList.Add(dto);
            }
            return dtosList;
        }

        public async Task FillDb(string text, int count, int userId)
        {
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                var entity = new HelperChatHistory()
                {
                    UserId = rnd.Next(26,34),
                    //UserId = userId,
                    Message = text,
                    Answer = text,
                };
                await _context.HelperChatHistory.AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }
    }
}
