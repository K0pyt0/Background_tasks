using System;

namespace Фоновая52
{
    class matrixWeather
    {
        enum Months { January, February, March, April, May, June, July, August, September, October, November, December };
        static int[] Lengths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        enum DaysAWeee { Mon, Tue, Wed, Thu, Fri, Sat, Sun };
        Months month;
        int day;
        int[,] temperature;

        matrixWeather()
        {
            month = Months.January;
            day = 0;
            temperature = new int[7, Lengths[(int)month] / 7 * 2 + 2];
            for (int i = 0; i <= Lengths[(int)month]; i++)
            {
                temperature[i % 7, i / 7 * 2] = i + 1;
                temperature[i % 7, i / 7 * 2 + 1] = GnrRnd(-20);
            }
        }

        matrixWeather(int day, Months month)
        {
            this.month = month;
            this.day = day;
            temperature = new int[7, (Lengths[(int)month] + day) / 7 * 2 + 2];
            for (int i = day; i <= Lengths[(int)month] + day; i++)
            {
                temperature[i % 7, i / 7 * 2] = i + 1 - day;
                temperature[i % 7, i / 7 * 2 + 1] = GnrRnd(40 - 10 * Math.Abs(7 - (int)month));
            }
        }

        void MoveTable(int day)
        {
            int[,] tmpTemperature = new int[(Lengths[(int)month] + day) / 7 * 2 + 2, 7];
            for (int i = day; i < Lengths[(int)month] + day; i++)
            {
                tmpTemperature[i % 7, i / 7 * 2] = i + 1 - day;
                tmpTemperature[i % 7, i / 7 * 2 + 1] = temperature[(i + this.day - day) % 7, (i + this.day - day) / 7 * 2 + 1];
            }
            temperature = tmpTemperature;
        }


        static public matrixWeather CreateMatrix()
        {
            matrixWeather mtrx;
            Console.WriteLine("Вы хотите ввести данные? y/n");
            if (Console.ReadLine() == "y")
            {
                try
                {
                    Console.WriteLine("Введите день начала месяца");
                    int day = int.Parse(Console.ReadLine());
                    if (day > 7) throw new Exception("в неделе не может быть больше 7 дней.");

                    Console.WriteLine("Введите номер месяца");
                    int num = int.Parse(Console.ReadLine()) - 1;
                    Months month;
                    if (num < 12) month = (Months)num - 1;
                    else throw new Exception("в году не может быть больше 12 месяцев.");

                    mtrx = new matrixWeather(day, month);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Кто-то опять накосячил: возникла ошибка: " + error.Message);
                    mtrx = new matrixWeather();
                }
            }
            else
            {
                mtrx = new matrixWeather();
                Console.WriteLine("Матрица создана. И таблетки нет");
            }
            return mtrx;
        }

        public void PrintMtrx()
        {
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{(DaysAWeee)i}\t");
            }
            Console.WriteLine();
            for (int i = 0; i < Lengths[(int)month] + day; i++)
            {
                if (i % 7 == 0) Console.WriteLine();
                Console.Write(temperature[i % 7, (i / 7) * 2]);
                Console.Write($" {temperature[i % 7, (i / 7) * 2 + 1]}\t");
            }
        }

        public int bgstJump()
        {
            int jump = 0;
            for (int i = day; i < Lengths[(int)month]; i++) if (Math.Abs(temperature[i % 7, (i / 7) * 2 + 1] - temperature[(i + 1) % 7, ((i + 1) / 7) * 2 + 1]) > jump) jump = Math.Abs(temperature[i % 7, (i / 7) * 2 + 1] - temperature[(i + 1) % 7, ((i + 1) / 7) * 2 + 1]);
            return jump;
        }

        public int bgstJump(out int dayNum, out int bgnTmp)
        {
            int jump = 0;
            dayNum = 0;
            bgnTmp = 0;
            for (int i = day; i < Lengths[(int)month]; i++)
            {
                if (Math.Abs(temperature[i % 7, (i / 7) * 2 + 1] - temperature[(i + 1) % 7, ((i + 1) / 7) * 2 + 1]) > jump)
                {
                    jump = Math.Abs(temperature[i % 7, (i / 7) * 2 + 1] - temperature[(i + 1) % 7, ((i + 1) / 7) * 2 + 1]);
                    dayNum = i + 1;
                    bgnTmp = temperature[i % 7, (i / 7) * 2 + 1];
                }
            }
            return jump;
        }

        public int beginningDay
        {
            get { return day; }
            set
            {
                try
                {
                    if (value < 8)
                    {
                        MoveTable(value - 1);
                        this.day = value - 1;

                    }
                    else throw new Exception("в неделе не может быть больше 7 дней.");
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Возникла ошибка: {error.Message}");
                }
            }
        }

        public string mtrxMonth
        {
            get { return month.ToString(); ; }
        }

        public int DaysNumber
        {
            get { return Lengths[(int)month]; }
        }

        public int ZeroDaysNumber
        {
            get
            {
                int count = 0;
                for (int i = day; i <= Lengths[(int)month] + day; i++)
                {

                    if (temperature[i % 7, i / 7 * 2 + 1] == 0) count++;
                }
                return count;
            }
        }

        public static int NoData
        {
            get {return -1000;}
        }

        static int GnrRnd(int diapBgn)
        {
            Random rnd = new Random();
            return rnd.Next(diapBgn, diapBgn + 21);
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            matrixWeather mtrx = matrixWeather.CreateMatrix();
            mtrx.PrintMtrx();
            Console.WriteLine(@"Какую задачу вы хотите испытать?
1. Вывести на экран день недели начала месяца
2. Изменить день недели начала месяца
3. Вывести месяц
4. Вывести на экран массив со всей температурой
5. Вывести на экран кол-во дней в месяце
6. Вывести на экран кол-во дней в месяце с температурой в 0 градусов
7. Вывести на экран максимальный скачок температуры за месяц
8. Вывести на экран максимальный скачок температуры за месяц с номером дня и температурой до скачка
9 - бесконечность. Закрыть программу");
            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine(mtrx.beginningDay + 1);
                    break;

                case "2":
                    Console.WriteLine("Введите номер нового дня начала месяца");
                    int day = int.Parse(Console.ReadLine());
                    mtrx.beginningDay = day;
                    break;

                case "3":
                    Console.WriteLine(mtrx.mtrxMonth);
                    break;

                case "4":
                    mtrx.PrintMtrx();
                    break;

                case "5":
                    Console.WriteLine(mtrx.DaysNumber);
                    break;

                case "6":
                    Console.WriteLine(mtrx.ZeroDaysNumber);
                    break;

                case "7":
                    Console.WriteLine(mtrx.bgstJump());   
                    break;

                case "8":
                    int fDay, temp;
                    int jump = mtrx.bgstJump(out fDay, out temp);
                    Console.WriteLine($"Максимальный скачок: {jump}\nНомер дня: {fDay}\nТемпература до скачка: {temp}");
                    break;

                default:
                    Console.WriteLine("Ты добрый человек. Спасибо");
                    break;
            }
            Console.ReadKey();
        }
    }
}
