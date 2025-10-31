using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ColeccionesDemo
{
    class Program
    {
        static void Main()
        {
            Title("Clase 4: Colecciones en C# - Demo");
            Console.WriteLine("¿Qué demo querés seleccionar?");

            string[] demos =
            {
                "Arreglos",
                "Listas",
                "Diccionarios",
                "Conjuntos",
                "Colas",
                "Pilas",
                "Finalizar Proceso"
            };
            var index = 1;
            string? input = "";
            bool finishProcess = false;

            while (!finishProcess)
            {
                foreach (var demo in demos)
                {
                    Console.WriteLine($"{index} - {demo}");
                    index++;
                }
                Console.Write("Ingresá un número (1-7): ");
                input = Console.ReadLine();
                index = 1;

                if (int.TryParse(input, out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            DemoArreglos();
                            break;
                        case 2:
                            DemoListas();
                            break;
                        case 3:
                            DemoDiccionarios();
                            break;
                        case 4:
                            DemoConjuntos();
                            break;
                        case 5:
                            DemoColas();
                            break;
                        case 6:
                            DemoPilas();
                            break;
                        case 7:
                            finishProcess = true;
                            break;
                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida.");
                }

                if (finishProcess)
                {
                    Console.WriteLine("\nFin. Presioná cualquier tecla para salir...");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        static void Title(string text)
        {
            Console.WriteLine($"═══{text}═══");
        }

        static void Section(string text)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {text} ---");
        }

        // ===== ARREGLOS =====
        static void DemoArreglos()
        {
            ////////////////////////////////////////////////////////////////////////////////// Unidimensional
            Section("Arreglos");
            // Unidimensional
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
            Console.WriteLine($"Unidimensional (Length={dias.Length}):");
            foreach (var dia in dias)
            {
                Console.WriteLine($"- {dia}");
            }
            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            ////////////////////////////////////////////////////////////////////////////////// Multidimensional
            Console.WriteLine("==== Multidimensional (2x3) ====");
            // Multidimensional (2x3)
            int[,] matriz = new int[2, 3] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            Console.WriteLine($"Multidimensional Rank={matriz.Rank}, GetLength(0)={matriz.GetLength(0)}, GetLength(1)={matriz.GetLength(1)}");

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            Console.WriteLine("================================");
            Console.WriteLine("==== Multidimensional (3x3) ====");
            // Multidimensional (3x3)
            int[,,] bigMatriz = new int[2, 3, 1] {
                {
                    { 1 }, { 2 }, { 3 }
                },
                {
                    { 4 }, { 5 }, { 6 }
                }
            };
            Console.WriteLine($"Multidimensional Rank={bigMatriz.Rank}, GetLength(0)={bigMatriz.GetLength(0)}, GetLength(1)={bigMatriz.GetLength(1)}, GetLength(2)={bigMatriz.GetLength(2)}");

            for (int i = 0; i < bigMatriz.GetLength(0); i++)
            {
                for (int j = 0; j < bigMatriz.GetLength(1); j++)
                {
                    for (int k = 0; k < bigMatriz.GetLength(2); k++)
                    {
                        Console.WriteLine($"matriz[{i},{j},{k}] = {bigMatriz[i, j, k]}");
                    }
                }
            }
            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            ////////////////////////////////////////////////////////////////////////////////// Jagged
            // Jagged (arreglo de arreglos)
            int[][] jagged = new int[3][];
            jagged[0] = new int[] { 1, 2 };
            jagged[1] = new int[] { 3, 4, 5 };
            jagged[2] = new int[0]; // vacío
            Console.WriteLine("Jagged:");
            for (int i = 0; i < jagged.Length; i++)
                Console.WriteLine($"Fila {i}: [{string.Join(", ", jagged[i])}]");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();
        }

        // ===== LIST<T> =====
        static void DemoListas()
        {
            Section("List<T>");
            var nombres = new List<string>();
            nombres.Add("Ana");
            nombres.AddRange("Juan", "Sofía", "Juan");
            Console.WriteLine($"Lista inicial (Count={nombres.Count}, Capacity={nombres.Capacity}): {string.Join(", ", nombres)}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            nombres.Remove("Juan"); // elimina la primera coincidencia
            nombres.Insert(0, "Luis");
            Console.WriteLine("Después de Remove/Insert: " + string.Join(", ", nombres));

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            ////////////////////////////////////////////////////////////////////////////////// LINQ (métodos de extensión)
            Console.WriteLine($"LINQ Distinct().OrderBy");
            var unicosOrdenados = nombres.Distinct().OrderBy(n => n);
            Console.WriteLine("Distinct + OrderBy: " + string.Join(", ", unicosOrdenados));

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            Console.WriteLine($"LINQ Any()");
            bool hayAna = nombres.Any(n => n == "Ana");
            Console.WriteLine($"Any == Ana ? {hayAna}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            ////////////////////////////////////////////////////////////////////////////////// ForEach (acción por elemento)
            Console.WriteLine($"Foreach()");
            nombres.ForEach(n => Console.WriteLine($"-> {n}"));

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            ////////////////////////////////////////////////////////////////////////////////// ToArray
            Console.WriteLine($"LINQ ToArray()");
            var arreglo = nombres.ToArray();
            Console.WriteLine("ToArray Length=" + arreglo.Length);

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();
        }

        // ===== DICTIONARY<TKey,TValue> =====
        static void DemoDiccionarios()
        {
            Section("Dictionary<TKey,TValue>");
            var inventario = new Dictionary<string, int>()
            {
                ["Manzanas"] = 10,
                ["Peras"] = 5
            };
            inventario.Add("Bananas", 12);

            Console.WriteLine("Claves: " + string.Join(", ", inventario.Keys));
            Console.WriteLine("Valores: " + string.Join(", ", inventario.Values));

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            if (inventario.TryGetValue("Manzanas", out int qty))
                Console.WriteLine($"Manzanas => {qty}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            Console.WriteLine($"Sumo cantidad a peras");
            inventario["Peras"] += 3;
            Console.WriteLine("Peras actualizadas => " + inventario["Peras"]);

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            Console.WriteLine($"Pregunto si existen naranjas");
            bool tieneNaranjas = inventario.ContainsKey("Naranjas");
            Console.WriteLine($"¿Hay Naranjas? {tieneNaranjas}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();
        }

        // ===== HASHSET<T> =====
        static void DemoConjuntos()
        {
            Section("HashSet<T>");
            var a = new HashSet<int>(new[] { 1, 2, 3, 4 });
            var b = new HashSet<int>(new[] { 3, 4, 5 });

            var union = new HashSet<int>(a);
            union.UnionWith(b);
            Console.WriteLine("UnionWith: {" + string.Join(", ", union.OrderBy(x => x)) + "}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            var inter = new HashSet<int>(a);
            inter.IntersectWith(b);
            Console.WriteLine("IntersectWith: {" + string.Join(", ", inter) + "}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            var colores = new HashSet<string>();
            colores.Add("rojo");
            colores.Add("azul");
            colores.Add("rojo"); // duplicado, se ignora
            Console.WriteLine("Elementos únicos: " + string.Join(", ", colores));

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();
        }

        // ===== QUEUE<T> =====
        static void DemoColas()
        {
            Section("Queue<T>");
            var q = new Queue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            Console.WriteLine("Queue: " + string.Join(", ", q));

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();

            var primero = q.Dequeue();
            Console.WriteLine($"Dequeue => {primero}, Peek => {q.Peek()}, Count => {q.Count}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();
        }

        // ===== STACK<T> =====
        static void DemoPilas()
        {
            Section("Stack<T>");
            var st = new Stack<string>();
            st.Push("uno");
            st.Push("dos");
            st.Push("tres");
            Console.WriteLine("Stack: " + string.Join(", ", st));
            var ultimo = st.Pop();
            Console.WriteLine($"Pop => {ultimo}, Peek => {st.Peek()}, Count => {st.Count}");

            Console.WriteLine($"Enter to continue");
            Console.ReadLine();
        }
    }
}