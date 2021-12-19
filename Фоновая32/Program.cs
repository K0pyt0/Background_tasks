using System;

namespace Фоновая32
{
    class MainClass
    {
        static int[] InputArr()
        {
            Console.Write("Введите длину массива");
            int N = int.Parse(Console.ReadLine());
            int[] arr = new int[N];
            for (int i = 0; i < N; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }
            return arr;
        }

        static int[] InputArr(int N)
        {
            int[] arr = new int[N];
            for (int i = 0; i < N; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }
            return arr;
        }

        static void PrintArr(int[] arr)
        {
            foreach (var i in arr)
            {
                Console.Write($"{i} ");
            }
        }

        static void SumBtwMinMax()
        {
            int[] arr = InputArr();


            int sum = 0;
            Console.WriteLine(sum);
        }

        static void Move()
        {
            Console.Write("Введите длину массива");
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите сдвиг");
            int k = int.Parse(Console.ReadLine());
            int[] arr = InputArr(N + k);

            for (int i = N; i >= 0; i--)
            {
                arr[i + k] = arr[i];
            }

            Console.WriteLine(arr);
            Console.WriteLine();
        }

        static void Common()
        {
            int[] arr1 = InputArr();
            int[] arr2 = InputArr();
            for(int i = 0; i < arr1.Length; i++)
            {
                bool isThereSame = false;
                foreach (var j in arr2)
                {
                    if (arr1[i] == j)
                    {
                        isThereSame = true;
                        break;
                    }
                }
                if(isThereSame)
                {
                    Console.Write($"{arr1[i]} ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите номер функции, которую вы хотите проверить");
            string ans = Console.ReadLine();
            switch(ans)
            {
                case 1
            }
            SumBtwMinMax();
        }
    }
}
