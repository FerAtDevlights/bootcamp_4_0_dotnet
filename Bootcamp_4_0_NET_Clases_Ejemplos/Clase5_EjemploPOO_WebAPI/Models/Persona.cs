namespace Clase5_EjemploPOO_WebAPI.Models
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public abstract int Edad { get; set; }

        public Persona() { }
        public Persona(int id, string nombre, int edad)
        {
            Id = id;
            Nombre = nombre;
            Edad = edad;
        }

        public virtual string NombreCompleto()
        {
            return Nombre + " " + Apellido;
        }

        public abstract string DefinirRol();
    }
}
