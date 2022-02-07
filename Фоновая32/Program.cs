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

        static void PrintArr(int[] arr)
        {
            foreach (int i in arr)
            {
                Console.Write($"{i} ");
            }
        }

        static void SumBtwMinMax()
        {
            int[] arr = InputArr();
            int max = arr[0];
            int min = arr[0];
            int maxind = 0;
            int minind = 0;
            int ind = 0;
            int sum = 0;

            foreach (int i in arr)
            {
                if (i > max)
                {
                    max = i;
                    maxind = ind;
                }
                if(i <= min)
                {
                    min = i;
                    minind = ind;
                }
                ind++;
            }
            for (int i = maxind; i < minind; i++) sum += arr[i];
            Console.WriteLine(sum);
        }

        static void CyclMove()
        {
            int[] arr = InputArr();
            Console.WriteLine("Введите сдвиг");
            int k = int.Parse(Console.ReadLine());

            for (int i = 0; i < k; i++)
            {
                int swapVar = arr[1];
                
                for (int j = 1; j < arr.Length - 1; j++)
                {
                    
                }
            }

            Console.WriteLine(arr);
            Console.WriteLine();
        }

        static void CommonOnly()
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
            int ans = int.Parse(Console.ReadLine());
            switch(ans)
            {
                case 1:
                    SumBtwMinMax();
                    break;
                case 2:
                    CyclMove();
                    break;
                case 3:
                    CommonOnly();
                    break;
                case 4:
                    break;
                default:
                    Console.WriteLine("Your input was THE WHAT");
                    break;
            }
            SumBtwMinMax();
        }
    }
}
