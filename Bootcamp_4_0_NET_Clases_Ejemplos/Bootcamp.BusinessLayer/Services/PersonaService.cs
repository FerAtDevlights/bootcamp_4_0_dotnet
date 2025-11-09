using Bootcamp.BusinessLayer.Interfaces;
using Bootcamp.DataAccessLayer.Data;
using Bootcamp.DataAccessLayer.DTOs;
using Bootcamp.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.BusinessLayer.Services
{
    public class PersonaService : IPersonaService
    {
        private BootcampDbContext _db;
        public PersonaService(BootcampDbContext db)
        {
            _db = db;
        }
        public async Task<PersonaToReturnDTO> CreatePersona(PersonaDTO personaDto)
        {
            var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var personaExists = await _db.Personas.FirstOrDefaultAsync(x => x.Dni == personaDto.Dni);
                if (personaExists != null)
                {
                    throw new Exception("La persona con el DNI proporcionado ya existe.");
                }

                var newPersona = new Persona
                {
                    Nombre = personaDto.Nombre,
                    Apellido = personaDto.Apellido,
                    Direccion = personaDto.Direccion,
                    Edad = personaDto.Edad,
                    Dni = personaDto.Dni
                };

                await _db.Personas.AddAsync(newPersona);
                await _db.SaveChangesAsync();
                
                dbContextTransaction.Commit();

                return new PersonaToReturnDTO
                {
                    Nombre = newPersona.Nombre
                };
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                dbContextTransaction.Dispose();
                throw new Exception(ex.Message);
            }
        }

        //public async Task<string> AssignCarToPerson(AssignCarToPersonDTO data)
        //{
        //    try
        //    {
        //        await 
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
