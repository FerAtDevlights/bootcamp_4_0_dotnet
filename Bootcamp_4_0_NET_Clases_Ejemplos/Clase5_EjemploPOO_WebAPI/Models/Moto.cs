using Clase5_EjemploPOO_WebAPI.Interfaces;

namespace Clase5_EjemploPOO_WebAPI.Models
{
    public sealed class Moto: Vehiculo, IMotor
    {
        public Moto()
        {
            _capacidadMax = 1;
        }

        public void EncenderMotor()
        {
            Console.WriteLine("El motor de la moto está encendido.");
        }

        public void ApagarMotor()
        {
            Console.WriteLine("El motor de la moto está apagado.");
        }
    }
}
