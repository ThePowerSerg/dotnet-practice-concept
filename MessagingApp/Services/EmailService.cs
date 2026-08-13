using MessagingApp.Models;

namespace MessagingApp.Services
{
    // Defines how the email is going to be sent - Implementation
    public class EmailService : IEmailService
    {
        // Implements the SendMessage method
        public void SendEmail(UserProfile user, string message)
        {
            Console.WriteLine($"Email sent to {user.Email ?? "No Email found"}: {message}");
        }
    }
}