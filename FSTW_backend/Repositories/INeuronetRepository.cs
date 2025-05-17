using FSTW_backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace FSTW_backend.Repositories
{
    public interface INeuronetRepository
    {
        public Task AddMessage(string message, string answer);

        public Task<List<ChatHistory>> GetPrevMessages();
    }
}
