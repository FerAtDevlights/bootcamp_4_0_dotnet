using Clase5_EjemploPOO_WebAPI.Interfaces;

namespace Clase5_EjemploPOO_WebAPI.Models
{
    //NombreDeLaClase : [Herencia,][Interfaces,]
    public sealed class Auto : Vehiculo, IMotor
    {
        public Auto()
        {
            _capacidadMax = 4;
            Pasajeros = new List<Persona>();
        }

        public string AsignarConductor(Conductor conductor)
        {
            if (conductor.Edad < 18)
                return "El conductor debe ser mayor de edad.";
            if (Conductor != null)
                return "Ya hay un conductor asignado.";
            Conductor = conductor;
            return "Conductor asignado exitosamente.";
        }

        public string SubirPasajero(Pasajero pasajero)
        {
            if (Pasajeros.Count >= _capacidadMax)
                return "El auto ha alcanzado su capacidad máxima de pasajeros.";
            Pasajeros.Add(pasajero);
            return "Pasajero agregado exitosamente.";
        }

        public string BajarPasajero(Persona persona)
        {
            if (Pasajeros.Remove(persona))
                return "Pasajero bajado exitosamente.";
            return "El pasajero no se encuentra en el auto.";
        }

        public void EncenderMotor()
        {
            Console.WriteLine("El motor del auto está encendido.");
        }

        public void ApagarMotor()
        {
            Console.WriteLine("El motor del auto está apagado.");
        }
    }
}
