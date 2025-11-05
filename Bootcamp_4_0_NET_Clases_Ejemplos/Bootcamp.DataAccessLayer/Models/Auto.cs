using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.DataAccessLayer.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }

        //Relations
        public Persona Persona { get; set; }
        public int PersonaId { get; set; }
    }
}
