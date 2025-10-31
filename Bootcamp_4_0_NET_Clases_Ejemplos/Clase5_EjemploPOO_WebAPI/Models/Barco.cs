using Clase5_EjemploPOO_WebAPI.Interfaces;

namespace Clase5_EjemploPOO_WebAPI.Models
{
    public class Barco : Vehiculo, INavegable
    {
        public Barco()
        {
            _capacidadMax = 100;
            Pasajeros = new List<Persona>();
        }

        public void Anclar()
        {
            Console.WriteLine("El barco ha anclado.");
        }

        public void ApagarMotor()
        {
            Console.WriteLine("El motor del barco está apagado.");
        }

        public void EncenderMotor()
        {
            Console.WriteLine("El motor del barco está encendido.");
        }

        public void Navegar()
        {
            Console.WriteLine("El barco está navegando.");
        }
    }
}
