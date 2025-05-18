using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions;

namespace FSTW_backend.Models
{
    public class HelperChatHistory
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Answer { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
