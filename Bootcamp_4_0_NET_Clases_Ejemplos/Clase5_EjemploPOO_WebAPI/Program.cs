using Clase5_EjemploPOO_WebAPI.Helpers;
using Clase5_EjemploPOO_WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var product = new Product(1, "Producto A", 100);
Console.WriteLine("Producto inicializado con precio: " + product.Price);
Console.WriteLine("Precio con IVA: " + product.GetPriceWithIva());

//var conductor = new Conductor();
Console.WriteLine(Conductor.Saludar());
Persona pasajero = new Pasajero();
Persona conductor1 = new Conductor();
conductor1 = pasajero;
Conductor conductor2 = new Conductor();
Console.WriteLine(conductor2.Licencia);
//Console.WriteLine(conductor1.Licencia);

var auto = new Auto();
var moto = new Moto();
moto.TipoDeCombustible = TipoCombustible.Gasolina;
Console.WriteLine(VehiculoHelper.ObtenerCapacidadMaxima(auto));
Console.WriteLine(VehiculoHelper.ObtenerCapacidadMaxima(moto));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
