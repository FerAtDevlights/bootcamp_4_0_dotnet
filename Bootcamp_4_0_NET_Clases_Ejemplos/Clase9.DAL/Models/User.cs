using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bootcamp.DataAccessLayer.Models;

namespace Clase9.DAL.Models
{
    public class User: Persona
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } //Hashed Hash - Salt
    }
}
