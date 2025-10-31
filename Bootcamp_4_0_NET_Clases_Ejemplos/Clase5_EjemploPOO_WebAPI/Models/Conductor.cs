namespace Clase5_EjemploPOO_WebAPI.Models
{
    public class Conductor: Persona
    {
        private int _edad;
        public override int Edad 
        { 
            get => _edad; 
            set
            {
                if (value < 18)
                    throw new ArgumentException("El conductor debe ser mayor de edad.");
                _edad = value;
            } 
        }
        public string Licencia { get; set; }

        public override string DefinirRol()
        {
            return "Conductor";
        }

        public override string NombreCompleto()
        {
            return Apellido + ", " + Nombre;
        }

        public static string Saludar()
        {
            return $"Hola, soy el conductor del vehículo.";
        }
    }
}
