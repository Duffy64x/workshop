using System;

class Jacobi
{
    static void Main()
    {
        Console.Clear();

        // número de ecuaciones / variables
        int n;
        double[,] a; 
        // coeficientes de variables
        double[] b;  
        // valores constantes
        double[] x0; 
        // aproximación previa a valores variables
        double[] x; 
        // aproximación actual a valores variables
        double[] Difa; 
        // diferencia absoluta entre aproximaciones
        double tol; 
        // tolerancia
        int max;  
        // número máximo de iteraciones  
        int iteraciones;
        // número real de iteraciones requeridas
        bool Fulltol;
        // si los resultados están dentro de la tolerancia

        bool validN;
        string temp;
        string[] tempArray;
        double value;
        bool fin;

        while (true)
        {
            Console.Write("Ingrese el número de ecuaciones / variables (2 a 20): ");
            validN = int.TryParse(Console.ReadLine(), out n);
            if (validN && n > 1 && n < 21) break;
            Console.WriteLine("\nNúmero no válido, vuelva a ingresar\n");
        }

        Console.WriteLine("\nIngrese coeficientes variables, separados por espacios :\n");
        a = new double[n, n];


        for (int i = 0; i < n; i++)
        {
            do
            {
                Console.Write(" Ecuacion {0} : ", i + 1);
                temp = Console.ReadLine();
                tempArray = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempArray.Length != n)
                {
                    Console.WriteLine("\nNúmero de coeficientes no válido, vuelva a ingresar\n");
                    validN = false;
                    continue;
                }
                for (int j = 0; j < n; j++)
                {
                    validN = double.TryParse(tempArray[j], out value);
                    if (!validN)
                    {
                        Console.WriteLine("\nLa línea contiene un número no válido, vuelva a ingresar la línea completa\n");
                        break;
                    }
                    else if (j == i && value == 0)
                    {
                        Console.WriteLine("\nLa diagonal principal no puede contener coeficientes cero, vuelva a ingresar la línea completa\n");
                        validN = false;
                        break;
                    }
                    else
                    {
                        a[i, j] = value;
                    }
                }
            }
            while (!validN);
        }

        Console.WriteLine("\nIngrese valores constantes, separados por espacios\n");
        b = new double[n];

        do
        {
            Console.Write(" Para todas las ecuaciones : ");
            temp = Console.ReadLine();
            tempArray = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tempArray.Length != n)
            {
                Console.WriteLine("\nNúmero de valores constantes no válido, vuelva a ingresar\n");
                validN = false;
                continue;
            }
            for (int i = 0; i < n; i++)
            {
                validN = double.TryParse(tempArray[i], out value);
                if (!validN)
                {
                    Console.WriteLine("\nLa línea contiene un número no válido, vuelva a ingresar la línea completa\n");
                    break;
                }
                else
                {
                    b[i] = value;
                }
            }

        }
        while (!validN);

        Console.WriteLine("\nIngrese aproximaciones iniciales, separadas por espacios\n");
        x0 = new double[n];
        x = new double[n];
        Difa = new double[n];

        do
        {
            Console.Write(" Para todas las variables : ");
            temp = Console.ReadLine();
            tempArray = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tempArray.Length != n)
            {
                Console.WriteLine("\nNúmero de aproximaciones no válido, vuelva a ingresar\n");
                validN = false;
                continue;
            }
            for (int i = 0; i < n; i++)
            {
                validN = double.TryParse(tempArray[i], out value);
                if (!validN)
                {
                    Console.WriteLine("\nLa línea contiene un número no válido, vuelva a ingresar la línea completa\n");
                    break;
                }
                else
                {
                    x0[i] = value;
                }
            }

        }
        while (!validN);

        Console.WriteLine();

        while (true)
        {
            Console.Write("Ingrese la tolerancia para todas las variables (Mayor a 0) : ");
            validN = double.TryParse(Console.ReadLine(), out tol);
            if (validN && tol > 0) break;
            Console.WriteLine("\nNúmero no válido, vuelva a ingresar\n");
        }

        Console.WriteLine();

        while (true)
        {
            Console.Write("Ingrese el número máximo de iteraciones (5 a 99) : ");
            validN = int.TryParse(Console.ReadLine(), out max);
            if (validN && max > 4 && max < 100) break;
            Console.WriteLine("\nNúmero no válido, vuelva a ingresar\n");
        }

        Console.WriteLine();
        iteraciones = max;
        Fulltol = false;

        for (int iteracion = 1; iteracion <= max; iteracion++)
        {
            fin = true;
            for (int i = 0; i < n; i++)
            {
                x[i] = b[i];
                for (int j = 0; j < n; j++)
                {
                    if (j == i) continue;
                    x[i] -= a[i, j] * x0[j];
                }
                x[i] /= a[i, i];
                Difa[i] = Math.Abs(x[i] - x0[i]);
                if (fin && Difa[i] > tol) fin = false;
            }
            if (fin)
            {
                iteraciones = iteracion;
                Fulltol = true;
                break;
            }
            Array.Copy(x, x0, n);
        }

        Console.WriteLine("Los valores aproximados de las variables son :\n");
        for (int i = 1; i <= n; i++)
        {
            Console.Write(" x{0} = {1:F5}", i, x[i - 1]); // mostrar a 5 dp
            Console.WriteLine();
        }

        Console.WriteLine("\nNumero de iteraciones : {0}", iteraciones);
        Console.WriteLine("Las aproximaciones están dentro de la tolerancia. : {0}", Fulltol);

        Console.Write("\nPresione cualquier tecla para salir del programa\n");
        Console.ReadKey();

    }
}