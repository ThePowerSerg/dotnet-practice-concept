using MessagingApp.Data;
using MessagingApp.Models;

namespace MessagingApp.Controllers
{
    public class UserProfileController(MessagingAppContext context)
    {
        // Get list
        public List<UserProfile> GetUserProfileList()
        {
            var userProfileList = context.UserProfiles.ToList();
            return userProfileList;
        }

        // Get By ID
        public UserProfile GetUserProfileByID(int id)
        {
            var userProfile = context.UserProfiles.FirstOrDefault(x => x.Id == id) ?? throw new NotImplementedException("Id Not Found");
            return userProfile;
        }

        // Add new user

        // Update user
    }
}