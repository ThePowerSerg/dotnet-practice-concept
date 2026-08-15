using MessagingAPI.Models;
using Microsoft.EntityFrameworkCore;
using ReviewAPI.Data;
using ReviewAPI.Dtos;

namespace ReviewAPI.Services
{
    public class UserProfileService(AppDbContext context) : IUserProfileService
    {
        // Get a list of user profiles from the database and pass them as a list to the DTO
        public async Task<IEnumerable<UserProfileDto>> GetUserProfilesAsync()
        {
            return await context.UserProfiles
                .AsNoTracking()
                .Select(userProfile => new UserProfileDto
                {
                    Id = userProfile.Id,
                    UserName = userProfile.UserName,
                    Email = userProfile.Email,
                    PhoneNumber = userProfile.PhoneNumber,
                    Country = userProfile.Country
                })
                .ToListAsync();
        }
        // Get the user profile by Id from the database and pass it to the DTO
        public Task<UserProfileDto?> GetUserProfileByIdAsync(int id) =>
            context.UserProfiles
            .AsNoTracking()
            .Where(userProfile => userProfile.Id == id)
            .Select(userProfile => new UserProfileDto
            {
                Id = userProfile.Id,
                    UserName = userProfile.UserName,
                    Email = userProfile.Email,
                    PhoneNumber = userProfile.PhoneNumber,
                    Country = userProfile.Country

            })
            .FirstOrDefaultAsync();

        public UserProfileDto AddUserProfile(UserProfileDto userProfile)
        {
            var profile = new UserProfile
            {
                UserName = userProfile.UserName,
                Email = userProfile.Email,
                PhoneNumber = userProfile.PhoneNumber
            };
            context.UserProfiles.Add(profile);
            context.SaveChanges();

            return userProfile;
        }
    }
}