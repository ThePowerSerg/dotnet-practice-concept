using MessagingApp.Models;

namespace MessagingApp.Services
{
    public class SMSService : ISMSService
    {
        public void SendSMS(UserProfile user, string message)
        {
            Console.WriteLine($"SMS sent to: {user.PhoneNumber ?? "No Phone Number found"}: {message}");
        }
    }
}