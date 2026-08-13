using MessagingApp.Models;
using MessagingApp.Services;

namespace MessagingApp.Controllers
{
    // consuming class - coordinates which services will be consumed by the UI and communicates with the UI. 
    public class MessageController(IEmailService emailService, ISMSService sMSService)
    {
        public void SendEmail(UserProfile user, string message)
        {
            emailService.SendEmail(user, message);
        }
        public void SendSMS(UserProfile user, string message)
        {
            sMSService.SendSMS(user, message);
        }
    }
}