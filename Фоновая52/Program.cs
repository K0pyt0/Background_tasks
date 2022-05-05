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
            temperature = new int[7, Lengths[(int)month] / 7 + 1];
            for (int i = 0; i <= Lengths[(int)month]; i++)
            {
                temperature[i % 7, i / 7] = GnrRnd(-20);
            }
        }

        matrixWeather(int day, Months month)
        {
            this.month = month;
            this.day = day;
            temperature = new int[7, (Lengths[(int)month] + day) / 7 + 1];
            for (int i = 0; i < day; i++)
            {
                temperature[i, 0] = NoData;
            }
            for (int i = day; i < Lengths[(int)month] + day; i++)
            {
                temperature[i % 7, i / 7] = GnrRnd(40 - 10 * Math.Abs(7 - (int)month));
            }
        }

        matrixWeather(int day, int[]tArr)
        {
            month = Months.January;
            this.day = day;
            temperature = new int[7, (31 + day) / 7 + 1];
            for (int i = 0; i < day; i++)
            {
                temperature[i, 0] = NoData;
            }
            for (int i = day; i < 31 + day; i++)
            {
                temperature[i % 7, i / 7] = tArr[i - day];
            }
        }

        void MoveTable(int day)
        {
            int[,] tmpTemperature = new int[7, (Lengths[(int)month] + day) / 7 + 1];
            for (int i = 0; i < day; i++)
            {
                tmpTemperature[i, 0] = NoData;
            }
            for (int i = day; i < Lengths[(int)month] + day; i++)
            {
                tmpTemperature[i % 7, i / 7] = temperature[(i + this.day - day) % 7, (i + this.day - day) / 7];
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
                    if (day > 7 || day <= 0) throw new Exception("дня недели с таким номером не существует");

                    Console.WriteLine("Введите номер месяца");
                    int num = int.Parse(Console.ReadLine()) - 1;
                    Months month;
                    if (num < 12 && num > -1) month = (Months)num;
                    else throw new Exception("месяца с таким номером не существует");

                    mtrx = new matrixWeather(day - 1, month);
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

        static public matrixWeather CreateSpecialedMatrix()
        {
            int day = 0;
            try
            {
                Console.WriteLine("Введите день начала месяца");
                day = int.Parse(Console.ReadLine()) - 1;
                if (day > 6 || day < 0) throw new Exception("дня недели с таким номером не существует");
            }
            catch (Exception error)
            {
                Console.WriteLine("Кто-то опять накосячил: возникла ошибка: " + error.Message);
            }
            Console.WriteLine("Вы сами знали, на что шли.");
            int[] tArr = new int[31];
            for(int i = 1; i < 32; i++)
            {
                Console.WriteLine($"Введите температуру {i}-ого дня месяца");
                try
                {
                    int tmp = int.Parse(Console.ReadLine());
                    if (tmp < -20 || tmp > 0) throw new Exception("В юном месяце апреле в старом парке валил снег. Так, стоп");
                    else tArr[i - 1] = tmp;
                }
                catch(Exception error)
                {
                    Console.WriteLine(error.Message);
                    tArr[i - 1] = GnrRnd(-20);
                }
            }
            matrixWeather mtrx = new matrixWeather(day, tArr);
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
                Console.Write(i + 1 - day);
                if (temperature[i % 7, i / 7] != -1000) Console.Write($" {temperature[i % 7, i / 7]}\t");
                else Console.Write(" ND\t");
            }
            Console.WriteLine();
        }

        public int bgstJump()
        {
            int jump = 0;
            for (int i = day; i < Lengths[(int)month] + day; i++) if (Math.Abs(temperature[i % 7, i / 7] - temperature[(i + 1) % 7, (i + 1) / 7]) > jump) jump = Math.Abs(temperature[i % 7, i / 7] - temperature[(i + 1) % 7, (i + 1) / 7]);
            return jump;
        }

        public int bgstJump(out int dayNum, out int bgnTmp)
        {
            int jump = 0;
            dayNum = 0;
            bgnTmp = 0;
            for (int i = day; i < Lengths[(int)month] + day - 1; i++)
            {
                if (Math.Abs(temperature[i % 7, i / 7] - temperature[(i + 1) % 7, (i + 1) / 7]) > jump)
                {
                    jump = Math.Abs(temperature[i % 7, i / 7] - temperature[(i + 1) % 7, (i + 1) / 7]);
                    dayNum = i + 1 - day;
                    bgnTmp = temperature[i % 7, i / 7];
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
                    if (value <= 7)
                    {
                        MoveTable(value - 1);
                        day = value - 1;

                    }
                    else throw new Exception("в неделе не может быть больше 7 дней.");
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Возникла ошибка: {error.Message}");
                }
            }
        }

        public int[,] temperMtrx
        {
            get { return temperature; }
        }

        public string mtrxMonth
        {
            get { return month.ToString(); }
        }

        public int mtrxMonthNum
        {
            get { return (int)month; }
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

                    if (temperature[i % 7, i / 7] == 0) count++;
                }
                return count;
            }
        }

        public int this [int x, int y]
        {
            get
            {
                try
                {
                    if (temperature[x - 1, y - 1] != -1000 && (x - 1) * 7 + y - 1 < Lengths[(int)month] + day)
                        return temperature[x - 1, y - 1];
                    else throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Такого дня не существует");
                    return -1000;
                }
            }
            set
            {
                try
                {
                    if (value > (40 - 10 * Math.Abs(7 - (int)month)) && value < 60 - 10 * Math.Abs(7 - (int)month))
                        if (temperature[x - 1, y - 1] != -1000 && (x - 1) * 7 + y - 1 < Lengths[(int)month] + day) temperature[x - 1, y - 1] = value;
                        else throw new Exception("Такого дня не существует");
                    else throw new Exception($"В {month} не может быть температуры {value}");
                }
                catch(Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }

        public static bool operator >(matrixWeather obj1, matrixWeather obj2)
        {
            return obj1.mtrxMonthNum > obj2.mtrxMonthNum;
        }

        public static bool operator <(matrixWeather obj1, matrixWeather obj2)
        {
            return obj1.mtrxMonthNum < obj2.mtrxMonthNum;
        }

        public static matrixWeather operator ++(matrixWeather obj)
        {
            obj.beginningDay++;
            return obj;
        }

        public static matrixWeather operator --(matrixWeather obj)
        {
            obj.beginningDay--;
            return obj;
        }

        public static bool operator true(matrixWeather obj)
        {
            bool wasLower = false;
            for (int i = obj.beginningDay - 1; i < Lengths[obj.mtrxMonthNum] + obj.beginningDay - 1; i++)
            {
                if (obj.temperMtrx[i % 7, i / 7] < 0) wasLower = true;
            }
            return !wasLower;
        }

        public static bool operator false(matrixWeather obj)
        {
            bool wasHigher = false;

            for (int i = obj.beginningDay - 1; i < Lengths[obj.mtrxMonthNum] + obj.beginningDay - 1; i++)
            {
                if (obj.temperMtrx[i % 7, i / 7] < 0) wasHigher = true;
            }
            return !wasHigher;
        }

        public static bool operator &(matrixWeather obj1, matrixWeather obj2)
        {
            bool isThereDifference = false;
            for (int i = 0; i < 7; i++)
            {
                if (obj1.temperMtrx[i, 0] != obj2.temperMtrx[i, 0]) isThereDifference = true;
            }
            return !isThereDifference;
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
9 - что угодно. Закрыть программу");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine(mtrx.beginningDay + 1);
                    break;

                case "2":
                    Console.WriteLine("Введите номер нового дня начала месяца");
                    int day = int.Parse(Console.ReadLine());
                    mtrx.beginningDay = day;
                    mtrx.PrintMtrx();
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
                    Console.WriteLine($"с температурой в 0 градусов{mtrx.ZeroDaysNumber}");
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



            matrixWeather mtrx2 = matrixWeather.CreateMatrix();
            mtrx2.PrintMtrx();
            Console.WriteLine(@"Какую задачу вы хотите испытать?
1. Сравнить две матрицы 
2. Сдвинуть первую матрицу на день вправо
3. Сдвинуть первую матрицу на день влево
4. Проверить, опускалась ли темепература в первой матрице ниже 0 градусов
5. Проверить, совпадают ли данные первой недели в двух матрицах
6. Получить значение температуры за указанный день
7. Изменить значение температуры за указанный день
8 - что угодно. Закрыть программу");
            switch (Console.ReadLine())
            {
                case "1":
                    if (mtrx > mtrx2) Console.WriteLine("Первая матрица больше второй");
                    else if (mtrx < mtrx2) Console.WriteLine("Первая матрица меньше второй");
                    else Console.WriteLine("Матрицы равны");
                    break;

                case "2":
                    mtrx++;
                    mtrx.PrintMtrx();
                    break;

                case "3":
                    mtrx--;
                    mtrx.PrintMtrx();
                    break;

                case "4":
                    if (mtrx) Console.WriteLine("Температура в этот месяц не опускалась ниже нуля");
                    else Console.WriteLine("Температура в этот месяц опускалась ниже нуля");
                    break;

                case "5":
                    mtrx = matrixWeather.CreateSpecialedMatrix();
                    mtrx2 = matrixWeather.CreateSpecialedMatrix();
                    if (mtrx & mtrx2) Console.WriteLine("Данные первой недели совпадают");
                    else Console.WriteLine("Данные первой недели не совпадают");
                    break;

                case "6":
                    int x = int.Parse(Console.ReadLine());
                    int y = int.Parse(Console.ReadLine());
                    Console.WriteLine(mtrx[x, y]);
                    break;

                case "7":
                    x = int.Parse(Console.ReadLine());
                    y = int.Parse(Console.ReadLine());
                    mtrx[x, y] = int.Parse(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("Ты добрый человек. Спасибо");
                    break;
            }
        }
    }
}
