namespace FSTW_backend.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
