using MessagingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Data
{
    public class MessagingAppContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}