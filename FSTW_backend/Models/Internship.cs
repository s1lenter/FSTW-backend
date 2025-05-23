namespace FSTW_backend.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }  
        public string WorkFormat { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public string Link { get; set; }

        public string? Direction { get; set; }
        public bool isArchive { get; set; } = false;
        public string? IdFromHh { get; set; } = null;

        public List<Favorite> Favorite { get; set; }
        public Requirement Requirement { get; set; }
    }
}
