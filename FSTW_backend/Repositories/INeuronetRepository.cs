using FSTW_backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace FSTW_backend.Repositories
{
    public interface INeuronetRepository
    {
        public Task AddResumeMessage(string message, string answer);

        public Task AddDefaultMessage(string message, string answer, int userId);

        public Task<List<ChatHistory>> GetResumePrevMessages();

        public Task<List<HelperChatHistory>> GetDefaultPrevMessages();
    }
}
