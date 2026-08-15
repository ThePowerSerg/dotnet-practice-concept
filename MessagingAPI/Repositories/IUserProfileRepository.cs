using MessagingAPI.Models;

namespace ReviewAPI.Repositories
{
    // References the UserProfile Entity
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetUserProfilesAsync();
        Task<UserProfile?> GetUserProfileByIdAsync(int id);
        Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile);
    }
}
