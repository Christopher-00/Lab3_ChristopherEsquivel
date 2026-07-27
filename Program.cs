using System;
using System.Collections.Generic;


    class Program
    {
        static void Main(string[] args)
        {
            // Arreglo para llevar el contador de qué opciones se usan
            int[] usosOpciones = new int[7];
            List<int> numeros = new List<int>();
            bool hayDatos = false;
            int opc = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("=== PANEL DE CONTROL NUMERICO - LAB 3 ===");
                Console.WriteLine("1. Generador de tablas en lote");
                Console.WriteLine("2. Cargar numeros y estadisticas");
                Console.WriteLine("3. Busqueda avanzada");
                Console.WriteLine("4. Detectar numeros primos");
                Console.WriteLine("5. Piramide numerica");
                Console.WriteLine("6. Salir");
                Console.Write("Elige una opcion: ");

                
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("\nError: Debes ingresar un numero valido.");
                    Console.WriteLine("Presiona una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine();

                switch (opc)
                {
                    case 1:
                        usosOpciones[1]++;
                        TablasEnLote();
                        break;

                    case 2:
                        usosOpciones[2]++;
                        numeros = CargarEstadisticas(out hayDatos);
                        break;

                    case 3:
                        usosOpciones[3]++;
                        if (!hayDatos)
                        {
                            Console.WriteLine("Primero debes cargar datos (Modulo 2). Vamos para alla...");
                            Console.ReadKey();
                            numeros = CargarEstadisticas(out hayDatos);
                        }
                        if (hayDatos) BusquedaAvanzada(numeros);
                        break;

                    case 4:
                        usosOpciones[4]++;
                        if (!hayDatos)
                        {
                            Console.WriteLine("Primero debes cargar datos (Modulo 2). Vamos para alla...");
                            Console.ReadKey();
                            numeros = CargarEstadisticas(out hayDatos);
                        }
                        if (hayDatos) DetectarPrimos(numeros);
                        break;

                    case 5:
                        usosOpciones[5]++;
                        if (!hayDatos)
                        {
                            Console.WriteLine("Error: No hay datos cargados. Ejecuta el Modulo 2 primero.");
                        }
                        else
                        {
                            HacerPiramide(numeros);
                        }
                        break;

                    case 6:
                        usosOpciones[6]++;
                        Console.WriteLine("Resumen de uso del programa:");
                        int i = 1;
                        foreach (int c in usosOpciones)
                        {
                            if (i <= 6)
                            {
                                Console.WriteLine($"Opcion {i}: se uso {c} veces");
                            }
                            i++;
                        }
                        Console.WriteLine("\nSaliendo... Adios profesor.");
                        break;

                    default:
                        Console.WriteLine("Opcion fuera de rango (1 al 6).");
                        break;
                }

                if (opc != 6)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para volver al menu...");
                    Console.ReadKey();
                }

            } while (opc != 6);
        }

        // --- Modulo 1 ---
        static void TablasEnLote()
        {
            Console.WriteLine("--- 1. TABLAS EN LOTE ---");
            int ini = 0, fin = 0, limite = 0;

            do
            {
                Console.Write("Base inicial: ");
                int.TryParse(Console.ReadLine(), out ini);
                Console.Write("Base final: ");
                int.TryParse(Console.ReadLine(), out fin);

                if (ini <= 0 || fin <= 0 || ini > fin)
                {
                    Console.WriteLine("Valores malos. Deben ser positivos y la inicial menor o igual a la final.\n");
                }
            } while (ini <= 0 || fin <= 0 || ini > fin);

            do
            {
                Console.Write("Limite del multiplicador: ");
                int.TryParse(Console.ReadLine(), out limite);
            } while (limite <= 0);

            int contadorOps = 0;
            for (int b = ini; b <= fin; b++)
            {
                Console.WriteLine($"\nTabla del {b}:");
                for (int m = 1; m <= limite; m++)
                {
                    Console.WriteLine($"{b} x {m} = {b * m}");
                    contadorOps++;
                }
            }
            Console.WriteLine($"\nTotal de multiplicaciones hechas: {contadorOps}");
        }

        // --- Modulo 2 ---
        static List<int> CargarEstadisticas(out bool exito)
        {
            Console.WriteLine("--- 2. CARGA DE NUMEROS Y ESTADISTICAS ---");
            List<int> lista = new List<int>();
            int centinela = -9999;
            Console.WriteLine($"Usa el centinela {centinela} para terminar.\n");

            while (true)
            {
                Console.Write("Ingresa un numero: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int num))
                {
                    Console.WriteLine("Eso no es un numero, se ignora.");
                    continue; 
                }

                if (num == centinela) break; 

                lista.Add(num);
            }

            if (lista.Count == 0)
            {
                Console.WriteLine("No se metio nada.");
                exito = false;
                return lista;
            }

            exito = true;

            int suma = 0, max = lista[0], min = lista[0];
            int pares = 0, impares = 0, pos = 0, neg = 0, ceros = 0;
            int rachaAct = 1, rachaMax = 1;

            for (int i = 0; i < lista.Count; i++)
            {
                int n = lista[i];
                suma += n;
                if (n > max) max = n;
                if (n < min) min = n;
                if (n % 2 == 0) pares++; else impares++;
                if (n > 0) pos++; else if (n < 0) neg++; else ceros++;

                if (i > 0)
                {
                    if (lista[i] > lista[i - 1]) rachaAct++;
                    else rachaAct = 1;
                }
                if (rachaAct > rachaMax) rachaMax = rachaAct;
            }

            double prom = (double)suma / lista.Count;

            Console.WriteLine("\n--- RESULTADOS ---");
            Console.WriteLine("Suma: " + suma);
            Console.WriteLine("Promedio: " + prom);
            Console.WriteLine("Maximo: " + max + " | Minimo: " + min);
            Console.WriteLine($"Pares: {pares}, Impares: {impares}");
            Console.WriteLine($"Positivos: {pos}, Negativos: {neg}, Ceros: {ceros}");
            Console.WriteLine("Racha maxima en aumento: " + rachaMax);

            return lista;
        }

        // --- Modulo 3 ---
        static void BusquedaAvanzada(List<int> lista)
        {
            Console.WriteLine("--- 3. BUSQUEDA AVANZADA ---");
            Console.WriteLine("1. Busqueda exacta");
            Console.WriteLine("2. Primer mayor que X (sin negativos)");
            Console.Write("Opcion: ");
            int.TryParse(Console.ReadLine(), out int op);

            if (op == 1)
            {
                Console.Write("Numero a buscar: ");
                int.TryParse(Console.ReadLine(), out int buscado);
                int posEncontrada = -1;

                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i] == buscado)
                    {
                        posEncontrada = i;
                        break; 
                    }
                }

                if (posEncontrada != -1) Console.WriteLine("¡Hallado en la posicion: " + posEncontrada);
                else Console.WriteLine("No esta en la lista.");
            }
            else if (op == 2)
            {
                Console.Write("Ingresa X: ");
                int.TryParse(Console.ReadLine(), out int x);
                int resultado = -1;

                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i] < 0) continue; 

                    if (lista[i] > x)
                    {
                        resultado = lista[i];
                        break;
                    }
                }

                if (resultado != -1) Console.WriteLine("El primer mayor es: " + resultado);
                else Console.WriteLine("Ninguno cumple.");
            }
        }

        // --- Modulo 4 ---
        static void DetectarPrimos(List<int> lista)
        {
            Console.WriteLine("--- 4. NUMEROS PRIMOS ---");
            List<int> primos = new List<int>();

            foreach (int n in lista)
            {
                if (n <= 1) continue;

                bool esPrimo = true;
                for (int d = 2; d <= Math.Sqrt(n); d++)
                {
                    if (n % d == 0)
                    {
                        esPrimo = false;
                        break; 
                    }
                }
                if (esPrimo) primos.Add(n);
            }

            Console.WriteLine("Primos hallados: " + string.Join(", ", primos));
            Console.WriteLine("Total de primos: " + primos.Count);
        }

        // --- Modulo 5 ---
        static void HacerPiramide(List<int> lista)
        {
            Console.WriteLine("--- 5. PIRAMIDE ---");
            int cuentaPares = 0;
            foreach (int n in lista)
            {
                if (n % 2 == 0) cuentaPares++;
            }

            int altura = cuentaPares;
            if (altura <= 0)
            {
                Console.WriteLine("No hay suficientes pares para dar altura.");
                return;
            }

            for (int i = 1; i <= altura; i++)
            {
                for (int j = 0; j < altura - i; j++) Console.Write(" ");
                for (int k = 0; k < (2 * i - 1); k++) Console.Write("*");
                Console.WriteLine();
            }
        }
    }