using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt =>{
opt.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers(); 

var app = builder.Build();

app.UseCors(corspolicy => corspolicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.MapControllers();

app.Run();
