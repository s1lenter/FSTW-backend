namespace FSTW_backend.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string Hobbies { get; set; } = "Не указано";
        public string About { get; set; } = "Не указано";
        public string Experience { get; set; } = "Не указано";
        public string Skills { get; set; } = "Не указано";

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public List<Project> Projects { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<Education> Educations { get; set; }
        public List<Expirience> Expiriences { get; set; }
        public Contact Contact { get; set; }
    }
}
