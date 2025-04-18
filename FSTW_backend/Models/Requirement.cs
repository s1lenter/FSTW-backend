namespace FSTW_backend.Models
{
    public class Requirement
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string WorkFormat { get; set; }
        public int CoureseRequirement { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }

        public int InternshipId { get; set; }
        public Internship Internship { get; set; }
    }
}
