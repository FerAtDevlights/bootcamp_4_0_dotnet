using Bootcamp.BusinessLayer.Interfaces;
using Bootcamp.BusinessLayer.Services;

using Clase9.BL.Interfaces;
using Clase9.BL.Services;
using Clase9.DAL.Data;
using Clase9.DAL.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

//1. Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BootcampDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalBootcampDatabase"))
    //No usar SQL server y usar en su lugar PostgreSQL
);

//Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<BootcampDbContext>()
    .AddDefaultTokenProviders();

//Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        //policy.WithOrigins("https://localhos:3000")
        policy.AllowAnyOrigin()
            .AllowAnyHeader() //WithHeaders("content-type", "authorization")
            .AllowAnyMethod(); //WithMethods("GET", "DELETE", "PUT")
                               //AllowCredentials() 
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registrar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Clase 11 - Web API",
        Version = "v1",
        Description = "Web API creada para la clase 11 del Bootcamp .NET"
    });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese el token JWT con el prefijo 'Bearer '",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new string[] {}
        }
    });
});

//Add services to the container.
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IAutoService, AutoService>();
builder.Services.AddScoped<IAuthService, AuthService>(); //Scoped, Transient, Singleton

//Add Authentication
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

//Seed de Roles y Admin User
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    string[] roles = new string[] { "Admin", "User", "Manager" };
    foreach (var role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }

    //Crear un usuario Admin por defecto
    var adminName = "admin";
    var admin = await userManager.FindByNameAsync(adminName);
    if (admin == null)
    {
        var adminUser = new User
        {
            UserName = adminName,
            Email = "admin@email.com",
            Nombre = "Admin",
            Apellido = "User",
            Direccion = "Admin Street 123",
            Edad = 30,
            Dni = 123456
        };
        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if(result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Add Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("Frontend");
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//2.1 Run
app.Run();
