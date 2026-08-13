using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MessagingApp.Data
{
    // Used only by the `dotnet ef` CLI at design time (e.g. migrations add/update).
    // The tool can't build the app's normal Host/DI container (Program.cs never
    // calls host.Run()/RunAsync(), which is what it hooks into), so it falls back
    // to any IDesignTimeDbContextFactory<T> it finds instead.
    public class MessagingAppContextFactory : IDesignTimeDbContextFactory<MessagingAppContext>
    {
        public MessagingAppContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<MessagingAppContextFactory>(optional: true)
                .Build();

            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string"
                    + "'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<MessagingAppContext>();
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseSeeding(DbInitializer.SeedData);

            return new MessagingAppContext(optionsBuilder.Options);
        }
    }
}
