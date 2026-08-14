namespace MessagingAPI.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
    }
}