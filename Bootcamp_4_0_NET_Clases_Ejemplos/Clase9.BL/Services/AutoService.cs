using Bootcamp.BusinessLayer.Interfaces;
using Clase9.DAL.Data;
using Bootcamp.DataAccessLayer.DTOs;
using Bootcamp.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.BusinessLayer.Services
{
    public class AutoService : IAutoService
    {
        private BootcampDbContext _db;
        public AutoService(BootcampDbContext db)
        {
            _db = db;
        }
        public async Task<string> CreateCar(AutoDTO carToMap)
        {
            try
            {
                var patenteExsists = await _db.Autos.FirstOrDefaultAsync(x => x.Patente == carToMap.Patente);
                if(patenteExsists != null)
                {
                    throw new Exception("Car already exists");
                }

                await _db.Autos.AddAsync(new Auto
                {
                    Anio = carToMap.Anio,
                    Marca = carToMap.Marca,
                    Patente = carToMap.Patente,
                    Modelo = carToMap.Modelo
                });
                await _db.SaveChangesAsync();
                return "Car created successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> CreateListOfCars(List<AutoDTO> carsToMap)
        {
            try
            {
                var autosToAdd = new List<Auto>();
                foreach (var carToMap in carsToMap)
                {
                    var patenteExsists = await _db.Autos.FirstOrDefaultAsync(x => x.Patente == carToMap.Patente);
                    if (patenteExsists != null)
                    {
                        throw new Exception("Car already exists");
                    }

                    autosToAdd.Add(new Auto
                    {
                        Anio = carToMap.Anio,
                        Marca = carToMap.Marca,
                        Patente = carToMap.Patente,
                        Modelo = carToMap.Modelo
                    });
                }
                await _db.Autos.AddRangeAsync(autosToAdd);
                await _db.SaveChangesAsync();
                return "Car created successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteCar(int cardIdToDelete)
        {
            try
            {
                var carToDelete =  await _db.Autos.FirstOrDefaultAsync(x => x.Id == cardIdToDelete);
                if (carToDelete == null) throw new Exception("Car not found");

                _db.Autos.Remove(carToDelete);
                await _db.SaveChangesAsync();

                return "Car delete Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<bool> DeleteCarsByBrand(string brandToDelete)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<string> DeleteCarsByUser(int userId)
        {
            try
            {
                var persona = await _db.Personas.Include(x => x.Autos).FirstOrDefaultAsync(x => x.Id == userId);
                if(persona.Autos.Count > 0)
                {
                    _db.Autos.RemoveRange(persona.Autos);
                }

                _db.SaveChanges();

                return $"Cars from {persona.Nombre} were successfully removed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
