using System.Runtime.CompilerServices;

using Bootcamp.BusinessLayer.Interfaces;
using Bootcamp.DataAccessLayer.DTOs;

using Clase9.DAL.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bootcamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutosController : ControllerBase
    {
        private readonly IAutoService _autoService;
        private readonly BootcampDbContext _dbContext;

        public AutosController(IAutoService autoService, BootcampDbContext dbContext)
        {
            _autoService = autoService;
            _dbContext = dbContext;
        }
        // GET: api/<AutosController>
        [HttpPost("CreateCar")]
        public async Task<ActionResult> CreateCar(AutoDTO carToMap)
        {
            var response = await _autoService.CreateCar(carToMap);
            return Ok(response);
        }

        [HttpPost("CreateListOfCars")]
        public async Task<ActionResult> CreateListOfCars(List<AutoDTO> carsToMap)
        {
            var response = await _autoService.CreateListOfCars(carsToMap);
            return Ok(response);
        }

        [HttpDelete("DeleteCar")]
        public async Task<ActionResult> DeleteCar(int cardIdToDelete)
        {
            var response = await _autoService.DeleteCar(cardIdToDelete);
            return Ok(response);
        }

        [HttpDelete("DeleteCarsByUser/{userId}")]
        public async Task<ActionResult> DeleteCarsByUser(int userId)
        {
            var response = await _autoService.DeleteCarsByUser(userId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _dbContext.Autos.ToListAsync());
        }

        //TODO: Para la próxima clase
        //Configurar CORS
        //Configurar Swagger con Autenticación Bearer
        //Configurar Microsoft.Identity
        //Agregar algo más
    }
}
