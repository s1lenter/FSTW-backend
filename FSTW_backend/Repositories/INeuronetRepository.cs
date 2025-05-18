using FSTW_backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace FSTW_backend.Repositories
{
    public interface INeuronetRepository
    {
        public Task AddResumeMessage(int userId, int resumeId, string message, string answer);

        public Task AddDefaultMessage(string message, string answer, int userId);

        public Task<List<ChatHistory>> GetResumePrevMessages(int userId, int resumeId);

        public Task<List<HelperChatHistory>> GetDefaultPrevMessages(int userId);
    }
}
