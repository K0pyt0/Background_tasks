using System;

namespace Фоновые2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            double x = double.Parse(Console.ReadLine());
            double y = double.Parse(Console.ReadLine());
            double r1 = 1;
            double r2 = 2;
            double ans;

            if (y > 0)
            {
                if (Math.Pow(x, 2) + Math.Pow(x, 2) >= Math.Pow(r1, 2))
                {
                    if (Math.Pow(x, 2) + Math.Pow(x, 2) <= Math.Pow(r2, 2)) ans = 1 / (5 * x + y);
                    else ans = Math.Abs(1 - x * y);
                }
                else ans = Math.Abs(1 - x * y);
            }
            else ans = Math.Abs(1 - x * y);

            Console.WriteLine(ans);
        }
    }
}
