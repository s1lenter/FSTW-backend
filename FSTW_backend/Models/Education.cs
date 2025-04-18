namespace FSTW_backend.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Institute { get; set; }
        public int YearOfGraduation { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
