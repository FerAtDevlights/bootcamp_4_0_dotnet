using Bootcamp.BusinessLayer.Interfaces;
using Bootcamp.DataAccessLayer.Data;
using Bootcamp.DataAccessLayer.DTOs;
using Bootcamp.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bootcamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpPost("SavePersona")]
        public async Task<ActionResult<string>> SavePersona(PersonaDTO value)
        {
            var personaCreated = await _personaService.CreatePersona(value);
            return Ok($"Persona {personaCreated.Nombre} was successfully created");
        }

        //[HttpPost("AssignCar")]
        //public async Task<ActionResult<string>> AssignCar(AssignCarToPersonDTO value)
        //{
        //    var personaCreated = await _personaService.CreatePersona(value);
        //    return Ok($"Persona {personaCreated.Nombre} was successfully created");
        //}


        //[HttpPost("SaveAuto")]
        //public async void SaveAuto(AutoDTO value)
        //{
        //    var newAuto = new Auto
        //    {
        //        Anio = value.Anio,
        //        Marca = value.Marca,
        //        Modelo = value.Modelo,
        //        PersonaId = value.OwnerId
        //    };

        //    _db.Autos.Add(newAuto);

        //    _db.SaveChanges();
        //}


        //[HttpGet("GetPersonaAuto/{id}")]
        //public async Task<ActionResult<PersonaWithCarsDTO>> GetPersonaAuto(int id)
        //{
        //    var persona = await _db.Personas.Include(x => x.Autos).FirstOrDefaultAsync(x => x.Id == id);

        //    return Ok(new PersonaWithCarsDTO
        //    {
        //        Nombre = persona.Nombre,
        //        Apellido = persona.Apellido,
        //        Direccion = persona.Direccion,
        //        PersonaAutos = persona.Autos.Select(x => new AutoDTO
        //        {
        //            Anio = x.Anio,
        //            Marca = x.Marca,
        //            Modelo = x.Modelo
        //        }).ToList()
        //    });
        //}
    }
}
