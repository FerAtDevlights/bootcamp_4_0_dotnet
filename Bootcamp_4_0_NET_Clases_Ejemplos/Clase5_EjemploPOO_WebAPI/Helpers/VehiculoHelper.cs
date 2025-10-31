using Clase5_EjemploPOO_WebAPI.Models;

namespace Clase5_EjemploPOO_WebAPI.Helpers
{
    public static class VehiculoHelper
    {
        public static int ObtenerCapacidadMaxima(Vehiculo vehiculo)
        {
            return vehiculo switch
            {
                Models.Moto => 1,
                Models.BicicletaMotorizada => 1,
                Models.Bicicleta => 1,
                Models.Auto => 4,
                Models.Avion => 180,
                Models.Barco => 100,
                _ => 0
            };
        }

        public static void MostrarTipoVehiculo(Vehiculo vehiculo)
        {
            switch (vehiculo)
            {
                case Models.Moto:
                    Console.WriteLine("El vehículo es una moto.");
                    break;
                case Models.BicicletaMotorizada:
                    Console.WriteLine("El vehículo es una bicicleta motorizada.");
                    break;
                case Models.Bicicleta:
                    Console.WriteLine("El vehículo es una bicicleta.");
                    break;
                case Models.Auto:
                    Console.WriteLine("El vehículo es un auto.");
                    break;
                case Models.Avion:
                    Console.WriteLine("El vehículo es un avión.");
                    break;
                case Models.Barco:
                    Console.WriteLine("El vehículo es un barco.");
                    break;
                default:
                    Console.WriteLine("Tipo de vehículo desconocido.");
                    break;
            }
        }
    }
}
