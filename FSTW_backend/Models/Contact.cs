namespace FSTW_backend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Link> Links { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
