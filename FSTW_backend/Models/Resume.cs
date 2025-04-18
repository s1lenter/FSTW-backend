namespace FSTW_backend.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string Goal { get; set; }
        public string Hobbies { get; set; }
        public string About { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<Education> Educations { get; set; }
        public List<Expirience> Expiriences { get; set; }
        public Contact Contact { get; set; }
    }
}
