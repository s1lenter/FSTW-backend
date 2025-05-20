using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FSTW_backend.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "Имя";
        public string LastName { get; set; } = "Фамилия";
        public string MiddleName { get; set; } = "Отчество";
        public string Gender { get; set; } = "Не указано";
        public string Skills { get; set; } = "Не указано";
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        public string PhoneNumber { get; set; } = "Не указано";
        public string Vk { get; set; } = "Не указано";
        public string Telegram { get; set; } = "Не указано";
        public string GitHub { get; set; } = "Не указано";
        public string Linkedin { get; set; } = "Не указано";

        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
