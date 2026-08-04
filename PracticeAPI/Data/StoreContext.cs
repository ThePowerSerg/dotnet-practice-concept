using Microsoft.EntityFrameworkCore;
using PracticeAPI.Entities;

namespace PracticeAPI.Data
{

    // DBContext is the gateway between the database and the app entities
    public class StoreContext (DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products{ get; set; }
    }
}