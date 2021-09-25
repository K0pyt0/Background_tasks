using System;
//using Math;

namespace Практика2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());


            Console.WriteLine($"{((a - b) / (double)(c) + Math.Sqrt(c + a / Math.Sin(c / 4.0) - 0.2)):F2}");
            
        }
    }
}
//6 вариант