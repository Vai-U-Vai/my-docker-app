
using Countries_Cloud.DB;
using Countries_Cloud.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<Country>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();
