using MessagingApp.Controllers;

namespace MessagingApp.UI
{
    public class RunApp(MessageController controller, UserProfileController userProfileController)
    {
        public void Run()
        {
            Console.Write("Please enter a user ID from 1 to 2: ");
            var promptId = Console.ReadLine();

            bool convert = int.TryParse(promptId, out int id);

            if (!convert)
            {
                Console.WriteLine("Invalid user ID");
                return;
            }

            var user = userProfileController.GetUserProfileByID(id);

            if (user is null)
            {
                Console.WriteLine("User was not found in the database.");
                return;
            }

            controller.SendEmail(user, "Keep it flexible and DI via Email!");
            controller.SendSMS(user, "Keep it flexible and DI via SMS");
        }

        /*
           TODO - create an interface:
           1. Search the user by name and get email/phone number.
           2. Send an email.
           3. Send an SMS.
        */

    }
}
