namespace FSTW_backend.Dto.InternshipDto
{
    public class RequestInternshipDto
    {
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }
        public string WorkFormat { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public string Direction { get; set; }
        public string Link { get; set; }
    }
}
