using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ReviewAPI.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options) 
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}