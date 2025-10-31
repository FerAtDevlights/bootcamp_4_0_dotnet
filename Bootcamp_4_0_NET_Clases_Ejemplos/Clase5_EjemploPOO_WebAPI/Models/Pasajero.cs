namespace Clase5_EjemploPOO_WebAPI.Models
{
    public class Pasajero: Persona
    {
        private int _edad;
        public override int Edad 
        { 
            get => _edad; 
            set
            {
                if (value < 0)
                    throw new ArgumentException("La edad no puede ser negativa.");
                _edad = value;
            }
        }
        public override string DefinirRol()
        {
            return "Pasajero";
        }
    }
}
