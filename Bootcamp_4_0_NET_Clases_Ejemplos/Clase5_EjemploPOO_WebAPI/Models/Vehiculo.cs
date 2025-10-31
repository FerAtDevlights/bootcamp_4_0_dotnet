namespace Clase5_EjemploPOO_WebAPI.Models
{
    public abstract class Vehiculo
    {
        protected int _capacidadMax = 0;

        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public TipoCombustible TipoDeCombustible { get; set; }
        public Ubicacion UbicacionActual { get; set; }
        public Conductor Conductor { get; set; }
        public List<Persona> Pasajeros { get; set; }
    }

    public enum TipoCombustible
    {
        Gasolina,
        Diesel,
        Electrico,
        Hibrido
    }

    public struct Ubicacion
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        
        public Ubicacion(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

        public string Mostrar()
        {
            return $"Latitud: {Latitud}, Longitud: {Longitud}";
        }
    }
}
