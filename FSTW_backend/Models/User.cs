using System.ComponentModel.DataAnnotations.Schema;

namespace FSTW_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Favorite> Favorites { get; set; }
        public Profile Profile { get; set; }
        public List<Resume> Resumes { get; set; }
    }
}
