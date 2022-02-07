using System;

namespace Фоновая41
{
    class MainClass
    {
        //Копылов Иван
        static void PrintArr(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write($"{arr[j, i]} ");
                }
                Console.WriteLine();
            }
        }

        static void PrintArr(string[,] arr)
        {
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write($"{arr[j, i]} ");
                }
                Console.WriteLine();
            }
        }

        static void InputArrUli()
        {
            Console.WriteLine("Введите длину массива:");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите высоту массива:");
            int y = int.Parse(Console.ReadLine());
            int[,] arr = new int[x, y];
            int rowCount = 0;

            for (int i = 0; i < x * y - 1;)
            {
                //справа налево
                for(int j = 0; j < x - rowCount * 2; j++)
                {
                    Console.WriteLine("Введите элемент массива:");
                    arr[j + rowCount, rowCount] = int.Parse(Console.ReadLine());
                    i++;
                }
                //сверху вниз
                for(int j = 0; j < y - 2 - rowCount * 2; j++)
                {
                    Console.WriteLine("Введите элемент массива:");
                    arr[x - 1 - rowCount, j + 1 + rowCount] = int.Parse(Console.ReadLine());
                    i++;
                }
                //слева направо
                for(int j = 0; j < x - rowCount * 2; j++)
                {
                    Console.WriteLine("Введите элемент массива:");
                    arr[x - 1 - j - rowCount, y - 1 - rowCount] = int.Parse(Console.ReadLine());
                    i++;
                }
                //снизу вверх
                for (int j = 0; j < y - 2 - rowCount * 2; j++)
                {
                    Console.WriteLine("Введите элемент массива:");
                    arr[rowCount, y - j - 2 - rowCount] = int.Parse(Console.ReadLine());
                    i++;
                }
                rowCount++;
            }
            PrintArr(arr);
        }

        static void TransferDiag()
        {
            Console.WriteLine("Введите размеры массива:");
            int n = int.Parse(Console.ReadLine());
            int[,] arr = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) arr[j, i] = int.Parse(Console.ReadLine());
                Console.WriteLine();
            }
            for (int i = 0; i < n; i++)
            {
                if (i != n / 2)
                {
                    arr[i, i] = arr[i, i] + arr[n - 1 - i, i];
                    arr[n - 1 - i, i] = arr[i, i] - arr[n - 1 - i, i];
                    arr[i, i] = arr[i, i] - arr[n - 1 - i, i];
                }
            }
            PrintArr(arr);
        }

        static void PrintCheckMate()
        {
            Console.WriteLine("Введите размеры массива:");
            int n = int.Parse(Console.ReadLine());
            string[,] arr = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if ((j + i % 2) % 2 == 0) arr[j, i] = "*";
                    else arr[j, i] = " ";
                }
            }
            PrintArr(arr);
        }

        static void PrintSnowfl()
        {
            Console.WriteLine("Введите ширину массива:");
            int n = int.Parse(Console.ReadLine());
            string[,] arr = new string[n, n];
            for (int i = n / 2; i >= - n / 2; i--)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0) arr[j, n / 2 - i] = "*";
                    else if (j == n / 2 - i) arr[j, n / 2 - i] = "*";
                    else if (j == n / 2) arr[j, n / 2 - i] = "*";
                    else if (j == n / 2 + i) arr[j, n / 2 - i] = "*";
                    else arr[j, n / 2 - i] = " ";
                }
            }
            PrintArr(arr);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите букву функции на тест:");
            string ans = Console.ReadLine();
            switch (ans)
            {
                case "a":
                    Console.WriteLine("Это не самостоятльная функция, хотите проверить – используйте пункт b");
                    break;
                case "b":
                    InputArrUli();
                    break;
                case "c":
                    TransferDiag();
                    break;
                case "d1":
                    PrintCheckMate();
                    break;
                case "d2":
                    PrintSnowfl();
                    break;
            }
            Console.ReadKey();
        }
    }
}
