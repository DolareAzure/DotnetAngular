using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt =>{
opt.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

// Add services to the container.

builder.Services.AddControllers(); 

var app = builder.Build();


app.MapControllers();

app.Run();
