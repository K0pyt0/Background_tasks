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
            double r2 = 0.3;
            double ans;

            if (y > 0)
            {
                if (Math.Pow(x, 2) + Math.Pow(x, 2) <= Math.Pow(r2, 2))   
                {
                    if (Math.Pow(x, 2) + Math.Pow(x, 2) >= Math.Pow(r1, 2)) ans = Math.Sqrt(Math.Abs(2 * y + x));
                    else
                    {
                        if (y <= 0) ans = Math.Sqrt(Math.Abs(2 * y + x));
                        else ans = x / Math.Pow(y, 2);
                    }
                }
                else ans = x / Math.Pow(y, 2);
            }
            else ans = x / Math.Pow(y, 2);

            Console.WriteLine(ans);
        }
    }
}
