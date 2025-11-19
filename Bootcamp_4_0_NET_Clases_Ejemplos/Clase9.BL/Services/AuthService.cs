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

        public string GenerateJwtToken(int userId, string username, IList<string> roles) //(User user)
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

            //Agregar roles como claims
            foreach (var role in roles)
            {
                claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, role));
            }

            //Fecha de expiración
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpirationMinutes"]));

            //Crear el objeto token (JwtSecurityToken)
            //var token = new JwtSecurityToken(
            //    issuer: _config["Jwt:Issuer"],
            //    audience: _config["Jwt:Audience"],
            //    claims: claims,
            //    expires: expires,
            //    signingCredentials: signingCredentials
            //);

            //Crear tokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = signingCredentials
            };

            //"Escribir" el token y devolverlo
            var securityTokenHanlder = new JwtSecurityTokenHandler();
            var token = securityTokenHanlder.CreateToken(tokenDescriptor);
            return securityTokenHanlder.WriteToken(token);

            /*
             eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0N2NhZTA1ZC0wYmI5LTQzMTItODU2NS1kY2UwNTU4NDAyZTkiLCJzdWIiOiIzIiwidW5pcXVlX25hbWUiOiIzIiwidXNlcm5hbWUiOiJlbWFpbEBlbWFpbC5jb20iLCJpYXQiOjE3NjM1MDE3NTksIm5iZiI6MTc2MzUwMTc1OSwiZXhwIjoxNzYzNTA1MzU5fQ.n91aIcI721ubZ3W2JEXOeSqjvdI34gd9KlsD3LDsNT4
             eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJlZDQwMWNhZS1hYWE0LTQ4ZTktODMzMy1iMGNmMTRjZWYyMmYiLCJzdWIiOiIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IjMiLCJ1c2VybmFtZSI6ImVtYWlsQGVtYWlsLmNvbSIsImlhdCI6IjE4LzExLzIwMjUgMjE6MjQ6MDEiLCJleHAiOjE3NjM1MDQ2NDEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMjkiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTI5In0.KiSmF9T6jjH14x3sIo_MirwbCjaC7pFeMogJLDq80Jk
             */
        }
    }
}
