using ReviewAPI.Dtos;

namespace ReviewAPI.Services
{
    public interface IUserProfileService
    {
        // Get complete list
        Task<IEnumerable<UserProfileDto>> GetUserProfilesAsync();
        // Get specific user profile by id
        Task<UserProfileDto?> GetUserProfileByIdAsync(int id);
        Task<UserProfileDto> CreateUserProfileAsync(CreateUserProfileDto userProfile);
    }
}
