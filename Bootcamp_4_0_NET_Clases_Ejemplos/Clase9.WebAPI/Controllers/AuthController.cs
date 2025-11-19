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
using Microsoft.AspNetCore.Identity;

namespace Clase9.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly BootcampDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //POST: Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            //0. Validar credenciales vacías
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Credenciales vacías");
            }

            //1. Validar las credenciales del usuario (request.Username, request.Password)
            //1.1 Verifico que el username, el email, el DNI, el ID, (ALGO QUE SEA UNICO) existe en la BD
            //var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            var user = await _userManager.FindByNameAsync(request.Username);

            //1.1.1 Si no existe, return Unauthorized()
            if (user == null)
            {
                return Unauthorized();
            }
            //1.1.2 Si existe,
            //1.2 Verifico que la contraseña sea correcta
            //1.2.1 Si no es correcta, return Unauthorized()
            //bool wasVerified = _authService.VerifyPassword(user.PasswordHash, request.Password);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //var passwordVerified = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            //Obtener roles del usuario
            var roles = await _userManager.GetRolesAsync(user);

            //1.2.2 Si es correcta, genero un token JWT
            var token = _authService.GenerateJwtToken(user.Id, user.UserName, roles);

            return Ok(token);
        }

        //POST: Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            //0. Validar datos vacíos
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.RepeatPassword))
            {
                return BadRequest("Datos vacíos");
            }
            //1. Verificar que el usuario no exista (username, email, DNI, ID, etc)
            //1.1 Si existe, return BadRequest("El usuario ya existe")

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Nombre = request.Name,
                Apellido = request.LastName,
                Direccion = "",
                Edad = 0,
                Dni = 0
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            //if (_context.Users.Any(u => u.Email == request.Email))
            if(!result.Succeeded)
            {
                return BadRequest("El usuario ya existe");
            }
            //1.2 Si no existe, crear el usuario en la base de datos
            //1.2.1 Hashear la contraseña
            //var passwordHash = _authService.HashPassword(request.Password);
            //1.2.2 Guardar el usuario en la base de datos
            //var user = new User
            //{
            //    Nombre = request.Name,
            //    Apellido = request.LastName,
            //    Email = request.Email,
            //    Username = request.Email,
            //    Direccion = "",
            //    PasswordHash = passwordHash
            //};
            //_context.Users.Add(user);
            //_context.SaveChanges();

            //Agregar un rol por defecto al user
            await _userManager.AddToRoleAsync(user, "User");

            //2.1 Generar un token JWT
            var token = _authService.GenerateJwtToken(user.Id, user.UserName, ["User"]);
            return Ok(new { user, token });
        }
    }
}
