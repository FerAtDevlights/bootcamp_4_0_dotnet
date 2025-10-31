using Clase5_EjemploPOO_WebAPI.Interfaces;

namespace Clase5_EjemploPOO_WebAPI.Models
{
    public sealed class BicicletaMotorizada: Bicicleta, IMotor
    {
        public BicicletaMotorizada()
        {
            _capacidadMax = 1;
        }

        public void ApagarMotor()
        {
            Console.WriteLine("El motor de la bicicleta motorizada está apagado.");
        }

        public void EncenderMotor()
        {
            Console.WriteLine("El motor de la bicicleta motorizada está encendido.");
        }
    }
}
