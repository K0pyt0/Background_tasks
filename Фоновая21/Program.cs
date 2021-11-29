using System;

namespace Фоновая21
{
    class MainClass
    {
        static int CountZer(uint x1)
        {
            int count = 0;
            for (int i = 0; i < 32; i++)
            {
                if (((x1 >> i) & 0x1) == 0x0) count++;
            }
            return count;
        }

        static void PrintNoZer(int x2)
        {
            bool was1 = false;
            for (int i = 0; i < 32; i++)
            {
                if (((x2 << i) & 0x80000000) == 0x80000000) was1 = true;
                if (was1)
                {
                    if (((x2 << i) & 0x80000000) == 0x80000000) Console.Write(1);
                    else Console.Write(0);
                }
            }
            Console.WriteLine();

        }

        static void CyclMove(uint x1, uint n)
        {
            for (int i = 0; i < n; i++)
            {
                if (((x1 >> i) & 0x1) == 0x1) x1 = (x1 >> 1) | 0x80000000;
                else x1 >>= 1;
            }
            Output(x1);
        }

        static void Output(uint x)
        {
            for (int i = 0; i < 32; i++)
            {
                if (((x << i) & 0x80000000) == 0x80000000) Console.Write(1);
                else Console.Write(0);
            }
        }

        public static void Main(string[] args)
        {
            uint x1 = uint.Parse(Console.ReadLine());
            uint n = uint.Parse(Console.ReadLine());
            int x2 = int.Parse(Console.ReadLine());
            Console.WriteLine(CountZer(x1));
            PrintNoZer(x2);
            CyclMove(x1, n);
        }
    }
}
