namespace FSTW_backend.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }  
        public string Link { get; set; }

        public List<Favorite> Favorite { get; set; }
        public Requirement Requirement { get; set; }
    }
}
