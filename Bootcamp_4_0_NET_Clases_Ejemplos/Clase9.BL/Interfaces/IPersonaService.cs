using Bootcamp.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.BusinessLayer.Interfaces
{
    public interface IPersonaService
    {
        Task<PersonaToReturnDTO> CreatePersona(PersonaDTO personaDto);
    }
}
