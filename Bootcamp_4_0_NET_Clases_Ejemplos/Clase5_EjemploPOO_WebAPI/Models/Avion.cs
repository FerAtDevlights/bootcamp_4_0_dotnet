using Clase5_EjemploPOO_WebAPI.Interfaces;

namespace Clase5_EjemploPOO_WebAPI.Models
{
    public class Avion : Vehiculo, IMotor, IVolador
    {
        public Avion()
        {
            _capacidadMax = 150;
            Pasajeros = new List<Persona>();
        }

        public void ApagarMotor()
        {
            Console.WriteLine("El motor del avión está apagado.");
        }

        public void Aterrizar()
        {
            Console.WriteLine("El avión ha aterrizado.");
        }

        public void Despegar()
        {
            Console.WriteLine("El avión ha despegado.");
        }

        public void EncenderMotor()
        {
            Console.WriteLine("El motor del avión está encendido.");
        }
    }
}
