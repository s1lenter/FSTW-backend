namespace FSTW_backend.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public DateTime SavedAt { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int InternshipId { get; set; }
        public Internship Internship { get; set; }
    }
}
