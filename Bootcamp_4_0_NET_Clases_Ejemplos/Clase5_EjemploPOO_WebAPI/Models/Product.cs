namespace Clase5_EjemploPOO_WebAPI.Models
{
    public class Product
    {
        //Campos
        private decimal _price;

        //Propiedades automáticas
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio no puede ser cero o negativo.");
                _price = value;
            }
        }

        //Constructor
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Product() { }

        //Métodos
        public decimal GetPriceWithIva() => Price * 1.21m; // Asumiendo un impuesto del 21%
    }
}
