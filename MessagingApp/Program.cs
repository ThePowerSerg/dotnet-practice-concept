using MessagingApp.Controllers;
using MessagingApp.Data;
using MessagingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MessagingApp.UI;

// 1. Set up the DI container
var builder = Host.CreateApplicationBuilder(args);

// Layer in user-secrets explicitly (rather than relying on the Development-only
// auto-load behavior) so ConnectionStrings:DefaultConnection - deliberately left
// blank in appsettings.json - resolves from the secret store instead.
builder.Configuration.AddUserSecrets<Program>();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<MessagingAppContext>(options =>
    options.UseSqlServer(connectionString));

// Register the interface and its implementation
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ISMSService, SMSService>();
// Register the consumer class
builder.Services.AddTransient<MessageController>();
builder.Services.AddTransient<UserProfileController>();

// Register the app runner
builder.Services.AddTransient<RunApp>();

// 2. Build the host
using var host = builder.Build();

// 3. Resolve and run
// RunApp now depends on MessagingAppContext, which AddDbContext registers as
// scoped - resolve it from an explicit scope rather than the root provider.
using var scope = host.Services.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<RunApp>();
app.Run();
