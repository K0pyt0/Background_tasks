using System;
using System.IO;

namespace Фоновая42
{
    class MainClass
    {
        struct Lesson
        {
            public string classroom;
            public string teacher;
            public string group;
            public string subject;
            public string number;
        }

        static int GetListPosition(string number)
        {
            return int.Parse(number) * 3;
        }

        static Lesson InputLesson()
        {
            StreamReader rdr = new StreamReader("Bckgr42_Input");
            Lesson lesson;
            Console.WriteLine("Введите номер аудитории");
            lesson.classroom = rdr.ReadLine();
            Console.WriteLine("Введите имя учителя");
            lesson.teacher = rdr.ReadLine();
            Console.WriteLine("Введите номер группы");
            lesson.group = rdr.ReadLine();
            Console.WriteLine("Введите название предмета");
            lesson.subject = rdr.ReadLine();
            Console.WriteLine("Введите номер урока");
            lesson.number = rdr.ReadLine();
            return lesson;
        }

        static string[,] CreateFinalTmt(string[,] timetable)
        {
            string[,] fTimetable = new string[timetable.GetLength(0) + 1, timetable.GetLength(1)];
            fTimetable[0, 0] = "Урок";
            for (int i = 1; i < timetable.GetLength(1); i += 3)
            {
                fTimetable[0, i] = Convert.ToString(i / 3 + 1);
            }
            for (int i = 1; i < fTimetable.GetLength(0); i++)
            {
                for (int j = 0; j < timetable.GetLength(1); j++)
                {
                    fTimetable[i, j] = timetable[i - 1, j];
                }
            }
            return fTimetable;
        }

        static string[] InputGroupLessons(int LessonsAm)
        {
            string[] Timetable = new string[LessonsAm * 3 + 1];
            Lesson OneLesson = InputLesson();
            Timetable[0] = OneLesson.group;
            
            for(int i = 1; i <= LessonsAm; i++)
            {
                if (OneLesson.group == "") continue;
                if (Timetable[GetListPosition(OneLesson.number)] == "")
                {
                    Timetable[GetListPosition(OneLesson.number) - 2] = OneLesson.classroom;
                    Timetable[GetListPosition(OneLesson.number) - 1] = OneLesson.teacher;
                    Timetable[GetListPosition(OneLesson.number)] = OneLesson.subject;
                }
                else Console.WriteLine($"У класса {OneLesson.group} больше одного {OneLesson.number} урока");
                OneLesson = InputLesson();
            }
            return Timetable;
        }

        static string[,] CreateTimetable(int GroupsAm, int LessonsAm)
        {
            string[,] Timetable = new string[GroupsAm, LessonsAm];
            for (int i = 0; i < GroupsAm; i++)
            {
                for (int j = 0; j < LessonsAm * 3 + 1; j++) Timetable[i, j] = InputGroupLessons(LessonsAm)[j];
            }
            return Timetable;
        }

        static void CheckForErrors(string[,] Timetable)
        {
            for (int i = 1; i < Timetable.GetLength(1); i += 3)
            {
                for (int j = 0; j < Timetable.GetLength(0); j++)
                {
                    for (int k = j; k < Timetable.GetLength(0); k++)
                    {
                        if (Timetable[i, j] == Timetable[i, k])
                        {
                            Console.WriteLine($"Классы {Timetable[j, 0]} и {Timetable[k, 0]} занимают один и тот же кабинет ({Timetable[i, j]}) на {i} уроке");
                        }
                    }
                }
            }
            for (int i = 3; i < Timetable.GetLength(1); i += 3)
            {
                for (int j = 0; j < Timetable.GetLength(0); j++)
                {
                    for (int k = j; k < Timetable.GetLength(0); k++)
                    {
                        if (Timetable[i, j] == Timetable[i, k])
                        {
                            Console.WriteLine($"У классов {Timetable[j, 0]} и {Timetable[k, 0]} один и тот же учитель ({Timetable[i, j]}) на {i} уроке");
                        }
                    }
                }
            }
        }

        static void PrintByGroup(string[,] Timetable)
        {
            Console.WriteLine("Введите номер группы");
            string group = Console.ReadLine();
            bool wasThere = false;
            for (int i = 0; i < Timetable.GetLength(0); i++)
            {
                if (Timetable[i, 0] == group)
                {
                    wasThere = true;
                    for (int j = 1; j < Timetable.GetLength(1); j++)
                    {
                        Console.WriteLine(Timetable[i, j]);
                    }
                }
            }
            if (!wasThere) Console.WriteLine("Группа не найдена");
        }

        static void PrintByLesson(string[,] Timetable)
        {
            Console.WriteLine("Введите номер урока");
            int lessonNum = int.Parse(Console.ReadLine());
            if (lessonNum <= Timetable.GetLength(1))
            {
                for (int i = 0; i < Timetable.GetLength(0); i++)
                {
                    Console.Write($"{Timetable[i, lessonNum]} \t");
                }
            }
            else
            {
                Console.WriteLine("Урок не найден");
            }
        }

        static string[,] FillGaps(string[,] timetable)
        {
            Console.WriteLine("Введите номер класса");
            string group = Console.ReadLine();
            return timetable;
        }

        static void PrintTimetable(string[,] timetable)
        {
            string[,] fTimetable = CreateFinalTmt(timetable);
            for (int i = 0; i < fTimetable.GetLength(0); i++)
            {
                for (int j = 0; j < fTimetable.GetLength(1); j++)
                {
                    Console.Write($"{fTimetable[j, i]} \t");
                }
                Console.WriteLine();
            }
        }

        

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите количество групп");
            int GroupsAmount = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество уроков");
            int LessonsAmount = int.Parse(Console.ReadLine());
            string[,] timetable = CreateTimetable(GroupsAmount, LessonsAmount);
            CheckForErrors(timetable);
            Console.WriteLine("Как вы хотите вывести расписание? \nПо группе – 1 \nПо уроку – 2");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    PrintByGroup(timetable);
                    break;
                case 2:
                    PrintByLesson(timetable);
                    break;
            }
            PrintTimetable(timetable);
        }
    }
}
