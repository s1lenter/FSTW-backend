using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FSTW_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "User";

        [JsonIgnore]
        public List<Favorite> Favorites { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; }
        [JsonIgnore]
        public List<Resume> Resumes { get; set; }
        [JsonIgnore]
        public RefreshToken RefreshToken { get; set; }
        public List<HelperChatHistory> HelperChatHistory { get; set; }
        public List<ChatHistory> ChatHistory { get; set; }
    }
}
