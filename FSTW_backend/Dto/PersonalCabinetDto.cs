namespace FSTW_backend.Dto
{
    public class PersonalCabinetDto
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Faculty { get; set; }
        public string Course { get; set; }
        public string Skills { get; set; }

        public string PhoneNumber { get; set; }
        public string SocialNet { get; set; }
    }
}
