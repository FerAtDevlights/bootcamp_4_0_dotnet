using Bootcamp.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.BusinessLayer.Interfaces
{
    public interface IAutoService
    {
        Task<string> CreateCar(AutoDTO carToMap);
        Task<string> CreateListOfCars(List<AutoDTO> carsToMap);
        Task<string> DeleteCar(int cardIdToDelete);
        //Task<string> DeleteCarsByUser(int userId);

    }
}
