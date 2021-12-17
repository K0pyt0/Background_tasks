using System;

namespace Фоновая32
{
    class MainClass
    {
        static int[] InputArr(int N)
        {
            Console.Write("Введите длину массива");
            //int N = int.Parse(Console.ReadLine());
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
            Console.Write("Введите длину массива");
            int N = int.Parse(Console.ReadLine());
            int[] arr = InputArr(N);


            int sum = 0;
            Console.WriteLine(sum);
        }

        static int[] Move()
        {
            Console.Write("Введите длину массива");
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите сдвиг");
            int k = int.Parse(Console.ReadLine());
            int[] arr = InputArr(N + k);

            int[] finArr = new int[arr.Length + k];
            //for (int i = )
            return finArr;
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
            SumBtwMinMax();
        }
    }
}
