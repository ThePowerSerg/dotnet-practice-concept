using MessagingApp.Models;

namespace MessagingApp.Services
{
    // Abstraction layer used for DI registration, tests, etc. 
    public interface IEmailService
    {
        void SendEmail(UserProfile user, string message);
    }
}