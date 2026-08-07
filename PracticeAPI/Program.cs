using Microsoft.EntityFrameworkCore;
using PracticeAPI.Services;
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

var app = builder.Build();

app.MapControllers();

DbInitializer.InitDb(app);

app.Run();
