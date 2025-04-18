namespace FSTW_backend.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
