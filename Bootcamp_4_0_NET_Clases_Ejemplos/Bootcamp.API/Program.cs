using Bootcamp.BusinessLayer.Interfaces;
using Bootcamp.BusinessLayer.Services;
using Bootcamp.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BootcampDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalBootcampDatabase"))
    );

builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IAutoService, AutoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
