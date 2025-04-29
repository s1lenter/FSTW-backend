using System.Text.Json.Serialization;

namespace FSTW_backend.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "Не указано";
        public string LastName { get; set; } = "Не указано";
        public string MiddleName { get; set; } = "Не указано";
        public string Gender { get; set; } = "Не указано";
        public string Faculty { get; set; } = "Не указано";
        public string Course { get; set; } = "Не указано";
        public string Skills { get; set; } = "Не указано";
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public byte[] Avatar { get; set; } = new byte[0];

        public string PhoneNumber { get; set; } = "Не указано";
        public string SocialNet { get; set; } = "Не указано";

        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
