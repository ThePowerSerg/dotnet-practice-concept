
using MessagingAPI.Models;
using Microsoft.EntityFrameworkCore;
using ReviewAPI.Data;

namespace MessagingAPI.Data
{
    public class DbInitializer
    {
        // Registered via UseSeeding
        public static void SeedData(DbContext context, bool seed)
        {
            var messagingAppContext = (AppDbContext)context;

            if (!messagingAppContext.UserProfiles.Any())
            {
                // Seed User Profiles
                var userProfiles = new List<UserProfile>()
                {
                    new() {
                        UserName = "sergferreira81",
                        Email = "sergferreira81@gmail.com",
                        PhoneNumber = "7817332393",
                        Country = "United States"
                    },
                    new() {
                        UserName = "sergiof810",
                        Email = "sergiof810@outlook.com",
                        PhoneNumber = "7817332393",
                        Country = "United States"
                    }
                };

                messagingAppContext.UserProfiles.AddRange(userProfiles);
            }

            messagingAppContext.SaveChanges();
        }
    }
}