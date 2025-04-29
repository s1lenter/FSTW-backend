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
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public List<Favorite> Favorites { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; }
        [JsonIgnore]
        public List<Resume> Resumes { get; set; }
        [JsonIgnore]
        public RefreshToken RefreshToken { get; set; }
    }
}
