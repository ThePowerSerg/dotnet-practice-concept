using MessagingAPI.Models;
using ReviewAPI.Dtos;
using ReviewAPI.Repositories;

namespace ReviewAPI.Services
{
    // with the introduction of the repository, the service communicates with the repository instead of DbContext directly
    public class UserProfileService(IUserProfileRepository repository) : IUserProfileService
    {
        // Get a list of user profiles from the repository and map them to DTOs.
        public async Task<IEnumerable<UserProfileDto>> GetUserProfilesAsync()
        {
            var userProfiles = await repository.GetUserProfilesAsync();
            return userProfiles.Select(ToDto);
        }

        // Get the user profile by Id from the repository and map it to a DTO.
        public async Task<UserProfileDto?> GetUserProfileByIdAsync(int id)
        {
            var userProfile = await repository.GetUserProfileByIdAsync(id);
            return userProfile is null ? null : ToDto(userProfile);
        }

        public async Task<UserProfileDto> CreateUserProfileAsync(CreateUserProfileDto userProfile)
        {
            if (string.IsNullOrWhiteSpace(userProfile.UserName))
            {
                throw new ArgumentException("Username is required.", nameof(userProfile));
            }

            var profile = new UserProfile
            {
                UserName = userProfile.UserName,
                Email = userProfile.Email,
                PhoneNumber = userProfile.PhoneNumber,
                Country = userProfile.Country
            };

            var createdProfile = await repository.CreateUserProfileAsync(profile);
            return ToDto(createdProfile);
        }

        // maps the UserProfile (from database) to the Dto which is used by the controller
        private static UserProfileDto ToDto(UserProfile userProfile)
        {
            return new UserProfileDto
            {
                Id = userProfile.Id,
                UserName = userProfile.UserName,
                Email = userProfile.Email,
                PhoneNumber = userProfile.PhoneNumber,
                Country = userProfile.Country
            };
        }
    }
}
