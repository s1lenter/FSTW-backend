namespace FSTW_backend.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Faculty { get; set; }
        public string Course { get; set; }
        public string Skills { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Avatar { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
