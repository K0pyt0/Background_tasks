using System;

namespace Фоновая31
{
    class Program
    {
        static string Reverse(string num)
        {
            string tmp = "";
            for (int i = num.Length - 1; i >= 0; i--)
            {
                tmp += num[i];
            }
            return tmp;
        }

        static int ToNormType(char el)
        {
            return int.Parse(Convert.ToString(el));
        }

        static void ToOneLength(ref string num1, ref string num2)
        {
            num1 = Reverse(num1);
            num2 = Reverse(num2);
            while (num1.Length != num2.Length)
            {
                if (num1.Length > num2.Length) num2 += "0";
                else num1 += "0";
            }
        }

        static string Sum(string num1, string num2)
        {
            string ans = "";
            int leave = 0;
            for (int i = 0; i < num1.Length; i++)
            {
                int plus = ToNormType(num1[i]) + ToNormType(num2[i]) + leave;
                int add = plus % 10;
                ans += Convert.ToString(add);
                leave = plus / 10;
            }
            if (leave == 1)
            {
                ans += "1";
            }
            return Reverse(ans);
        }

        static string Decr(string num1, string num2)
        {
            string ans = "";
            int take = 0;
            for (int i = 0; i < num1.Length; i++)
            {
                int end = ToNormType(num1[i]) - ToNormType(num2[i]) - take;
                int add = 0;
                if (end >= 0)
                {
                    take = 0;
                    add = end;
                }
                else
                {
                    take = 1;
                    add = (end + 10) % 10;
                }
                ans += Convert.ToString(add);

            }
            return Reverse(ans);
        }

        static void Main(string[] args)
        {
            string num1 = Console.ReadLine();
            string num2 = Console.ReadLine();
            string oper = Console.ReadLine();
            ToOneLength(ref num1, ref num2);

            if (oper == "+") Console.WriteLine(Sum(num1, num2));
            if (oper == "-") Console.WriteLine(Decr(num1, num2));
            Console.ReadKey();
        }

    }
}