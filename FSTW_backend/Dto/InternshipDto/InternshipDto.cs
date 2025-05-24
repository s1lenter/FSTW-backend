namespace FSTW_backend.Dto.InternshipDto
{
    public class InternshipDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }
        public string WorkFormat { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public string Direction { get; set; }
        public string Link { get; set; }
        public bool IsArchive { get; set; }
    }
}
