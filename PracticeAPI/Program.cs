using Microsoft.EntityFrameworkCore;
using PracticeAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// StoreContext service
builder.Services.AddDbContext<StoreService>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

// FakeStoreService - in-memory stand-in for StoreContext, registered as scoped
// (one instance created per HTTP request, same lifetime AddDbContext uses by default)
//builder.Services.AddScoped<IFakeStoreService, FakeStoreService>();

var app = builder.Build();

app.MapControllers();

DbInitializer.InitDb(app);

app.Run();
