using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace FSTW_backend.Repositories.Neuro
{
    public interface INeuronetRepository
    {
        public Task AddResumeMessage(int userId, int resumeId, string message, string answer);

        public Task AddDefaultMessage(string message, string answer, int userId);

        public Task<List<ChatHistory>> GetResumePrevMessages(int userId, int resumeId);

        public Task<List<HelperChatHistory>> GetDefaultPrevMessages(int userId);

        public Task<List<NeuronetDto>> GetMessagesDefaultHistory(int userId, int count, int page);

        public Task<List<NeuronetDto>> GetMessagesResumeHistory(int userId, int count, int page);

        public Task FillDb(string text, int count, int userId);
    }
}
