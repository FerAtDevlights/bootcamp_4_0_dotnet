using System.Security.Cryptography;
using System.Text;

using Clase9.DAL.Data;

using Clase9.BL.Interfaces;
using Clase9.WebAPI.DTOs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using Clase9.DAL.Models;

namespace Clase9.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BootcampDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(BootcampDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        //POST: Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            //0. Validar credenciales vacías
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Credenciales vacías");
            }

            //1. Validar las credenciales del usuario (request.Username, request.Password)
            //1.1 Verifico que el username, el email, el DNI, el ID, (ALGO QUE SEA UNICO) existe en la BD
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            
            //1.1.1 Si no existe, return Unauthorized()
            if (user == null)
            {
                return Unauthorized();
            }
            //1.1.2 Si existe,
            //1.2 Verifico que la contraseña sea correcta
            //1.2.1 Si no es correcta, return Unauthorized()
            if (_authService.VerifyPassword(user.PasswordHash, request.Password))
            {
                return Unauthorized();
            }
            //1.2.2 Si es correcta, genero un token JWT
            var token = _authService.GenerateJwtToken(user.Id, user.Username);

            return Ok(token);
        }

        //POST: Register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto request)
        {
            //0. Validar datos vacíos
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.RepeatPassword))
            {
                return BadRequest("Datos vacíos");
            }
            //1. Verificar que el usuario no exista (username, email, DNI, ID, etc)
            //1.1 Si existe, return BadRequest("El usuario ya existe")
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("El usuario ya existe");
            }
            //1.2 Si no existe, crear el usuario en la base de datos
            //1.2.1 Hashear la contraseña
            var passwordHash = _authService.HashPassword(request.Password);
            //1.2.2 Guardar el usuario en la base de datos
            var user = new User
            {
                Nombre = request.Name,
                Apellido = request.LastName,
                Email = request.Email,
                Username = request.Email,
                PasswordHash = passwordHash
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            //2.1 Generar un token JWT
            var token = _authService.GenerateJwtToken(user.Id, user.Username);
            return Ok(new { user, token });
        }
    }
}
