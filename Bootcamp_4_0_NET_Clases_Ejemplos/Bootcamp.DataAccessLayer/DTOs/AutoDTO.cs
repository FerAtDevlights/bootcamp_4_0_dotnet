using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.DataAccessLayer.DTOs
{
    public class AutoDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        [MaxLength(8)]
        public string Patente { get; set; }
        public int Anio { get; set; }
    }
}
