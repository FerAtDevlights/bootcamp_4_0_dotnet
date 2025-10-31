using System.Diagnostics;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("=== Funciones en C# - Ejemplos ===\n");

        // 1) Declaración básica + retorno de valores
        Console.WriteLine("1) Declaración básica + retorno:");
        int suma = Sumar(3, 5);
        Console.WriteLine($"Sumar(3,5) = {suma}\n");
        Console.ReadLine();

        // 2) Parámetros por valor (default)
        Console.WriteLine("2) Parámetros por valor:");
        int x = 10;
        AumentarPorValor(x);
        Console.WriteLine($"x sigue valiendo 10 fuera de la función: {x}\n");
        Console.ReadLine();

        // 3) Parámetros por referencia (ref)
        Console.WriteLine("3) Parámetros por referencia (ref):");
        int y = 10;
        AumentarPorReferencia(ref y);
        Console.WriteLine($"y cambió fuera de la función por usar ref: {y}\n");
        Console.ReadLine();

        // 4) Parámetros de salida (out)
        Console.WriteLine("4) Parámetros de salida (out):");
        if (TryDividir(10, 2, out int cociente))
        {
            Console.WriteLine($"10 / 2 = {cociente}");
        }
        if (!TryDividir(10, 0, out _))
        {
            var tryResult = TryDividir(10, 0, out int testResult);
            Console.WriteLine($"No se puede dividir por cero (TryDividir devolvió {tryResult}, testResult es = {testResult}).\n");
        }
        Console.ReadLine();

        // 5) Parámetros opcionales y nombrados
        Console.WriteLine("5) Parámetros opcionales y nombrados:");
        decimal precio1 = CalcularPrecio(precioBase: 100m); // usa impuesto por defecto 21%
        decimal precio2 = CalcularPrecio(precioBase: 100m, descuento: 10m); // nombrado
        Console.WriteLine($"Precio1= {precio1}, Precio2 (con descuento)= {precio2}\n");
        Console.ReadLine();


        // 6) Procedimientos (void)
        Console.WriteLine("8) Procedimientos (void):");
        ImprimirTitulo("Curso .NET - Funciones");
        Console.WriteLine();
        Console.ReadLine();

        // 7) Sobrecarga de funciones
        Console.WriteLine("7) Sobrecarga de funciones:");
        Console.WriteLine($"Area de círculo (r=3): {Area(3)}");
        Console.WriteLine($"Area de rectángulo (3x5): {Area(3, 5)}\n");
        Console.ReadLine();

        // 8) Programación asíncrona: async/await (I/O simulado)
        Console.WriteLine("11) Async/Await (simulación de I/O con Task.Delay):");
        ObtenerClimaSimuladoAsync("Buenos Aires");
        Console.WriteLine("test");

        await ObtenerClimaSimuladoAsync("Corrientes");
        Console.WriteLine("test");
        Console.ReadLine();

        Console.WriteLine("\n=== Fin de la demo ===");
        Console.ReadLine();
    }



    // -------------------------------------------------------------
    // 1) Declaración básica + retorno
    // -------------------------------------------------------------
    public static int Sumar(int a, int b) => a + b;

    // -------------------------------------------------------------
    // 2) Parámetros por valor (default)
    // Los parámetros se copian; los cambios no afectan a la variable original.
    // -------------------------------------------------------------
    public static void AumentarPorValor(int n)
    {
        n += 10; // sólo cambia la copia local
    }

    // -------------------------------------------------------------
    // 3) Parámetros por referencia (ref)
    // Los cambios afectan a la variable original.
    // -------------------------------------------------------------
    public static void AumentarPorReferencia(ref int n)
    {
        n += 10;
    }

    // -------------------------------------------------------------
    // 4) Parámetros de salida (out)
    // out obliga a asignar dentro del método antes de usarlo afuera.
    // -------------------------------------------------------------
    public static bool TryDividir(int numerador, int denominador, out int resultado)
    {
        if (denominador == 0)
        {
            resultado = 0;
            return false;
        }
        resultado = numerador / denominador;
        return false;
    }

    // -------------------------------------------------------------
    // 5) Parámetros opcionales y nombrados
    // "impuesto" y "descuento" tienen valores por defecto.
    // Llamadas pueden usar argumentos nombrados para claridad.
    // -------------------------------------------------------------
    public static decimal CalcularPrecio(decimal precioBase, decimal impuesto = 0.21m, decimal descuento = 0m)
    {
        var precioConImpuesto = precioBase * (1 + impuesto);
        var precioFinal = precioConImpuesto - descuento;
        return decimal.Round(precioFinal, 2);
    }

    // -------------------------------------------------------------
    // 6) Sobrecarga de funciones: mismo nombre, distinta firma
    // -------------------------------------------------------------
    public static double Area(double radio) // área de círculo
    {
        return Math.PI * radio * radio;
    }

    public static double Area(double ancho, double alto) // área de rectángulo
    {
        return ancho * alto;
    }

    // -------------------------------------------------------------
    // 7) Procedimientos (void)
    // -------------------------------------------------------------
    public static void ImprimirTitulo(string texto)
    {
        Console.WriteLine(new string('=', texto.Length));
        Console.WriteLine(texto);
        Console.WriteLine(new string('=', texto.Length));
    }

    // -------------------------------------------------------------
    // 8) Async/Await: simulación de I/O (p.ej., consulta HTTP/DB)
    // -------------------------------------------------------------
    public static async Task ObtenerClimaSimuladoAsync(string ciudad)
    {
        await Task.Delay(200); // simula latencia de red
        Console.WriteLine($"[Async] Clima simulado para {ciudad}: 23°C despejado");
    }
}
