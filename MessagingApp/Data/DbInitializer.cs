using MessagingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Data
{
    public static class DbInitializer
    {
        // Registered via UseSeeding on MessagingAppContextFactory, so this only
        // runs when triggered by an EF command (e.g. `dotnet ef database update`)
        // or Database.EnsureCreated() - never on a plain `dotnet run`.
        public static void SeedData(DbContext context, bool _)
        {
            var messagingAppContext = (MessagingAppContext)context;

            if (messagingAppContext.UserProfiles.Any()) return;

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

            messagingAppContext.SaveChanges();
        }
    }
}
