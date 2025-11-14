using Bootcamp.BusinessLayer.Interfaces;
using Bootcamp.BusinessLayer.Services;

using Clase9.BL.Interfaces;
using Clase9.BL.Services;

using Clase9.DAL.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

//1. Builder
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
builder.Services.AddScoped<IAuthService, AuthService>(); //Scoped, Transient, Singleton

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
//builder.Services.AddAuthorization((options) =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "admin"));
//});

//2. App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//2.1 Run
app.Run();
