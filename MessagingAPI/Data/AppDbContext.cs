using MessagingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ReviewAPI.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}