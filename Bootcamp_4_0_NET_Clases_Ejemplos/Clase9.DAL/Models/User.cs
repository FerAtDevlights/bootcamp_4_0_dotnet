using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bootcamp.DataAccessLayer.Models;

using Microsoft.AspNetCore.Identity;

namespace Clase9.DAL.Models
{
    public class User: IdentityUser<int> //Persona
    {
        //Id String GUID
        //public string Username { get; set; }
        //public string Email { get; set; }
        //public string PasswordHash { get; set; } //Hashed Hash - Salt
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        [MaxLength(6)]
        public int Dni { get; set; }

        public ICollection<Auto> Autos { get; set; }
    }
}
