using MessagingApp.Models;

namespace MessagingApp.Services
{
    public interface ISMSService
    {
        void SendSMS(UserProfile user, string message);
    }
}