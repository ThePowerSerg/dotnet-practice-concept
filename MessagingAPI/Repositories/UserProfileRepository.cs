using MessagingAPI.Models;
using Microsoft.EntityFrameworkCore;
using ReviewAPI.Data;

namespace ReviewAPI.Repositories
{
    public class UserProfileRepository(AppDbContext context) : IUserProfileRepository
    {
        public async Task<IEnumerable<UserProfile>> GetUserProfilesAsync()
        {
            return await context.UserProfiles
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<UserProfile?> GetUserProfileByIdAsync(int id) =>
            context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(userProfile => userProfile.Id == id);

        public async Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile)
        {
            context.UserProfiles.Add(userProfile);
            await context.SaveChangesAsync();

            return userProfile;
        }
    }
}
