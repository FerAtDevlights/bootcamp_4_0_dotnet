using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Azure.Core;

using Clase9.BL.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Clase9.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string HashPassword(string plainPassword)
        {
            using var sha = SHA256.Create();
            var hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
            return Convert.ToBase64String(hashedPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return hashedPassword == HashPassword(plainPassword);
        }

        public string GenerateJwtToken(int userId, string username) //(User user)
        {
            //Credenciales de seguridad
            var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var key = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Claims
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Name, userId.ToString()),
                
                //Claim personalizado
                new System.Security.Claims.Claim("username", username),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            //Fecha de expiración
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpirationMinutes"]));

            //Crear el objeto token (JwtSecurityToken)
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            //"Escribir" el token y devolverlo
            var securityTokenHanlder = new JwtSecurityTokenHandler();
            return securityTokenHanlder.WriteToken(token);
        }
    }
}
