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
            public int number;
        }

        
        //task 1
        static Lesson InputLesson(StreamReader rdr)
        {
            Lesson lesson;
            lesson.classroom = rdr.ReadLine();
            //2
            Console.WriteLine(lesson.classroom);
            lesson.teacher = rdr.ReadLine();
            Console.WriteLine(lesson.teacher);
            lesson.group = rdr.ReadLine();
            Console.WriteLine(lesson.group);
            lesson.subject = rdr.ReadLine();
            Console.WriteLine(lesson.subject);
            lesson.number = int.Parse(rdr.ReadLine());
            Console.WriteLine(lesson.number);
            return lesson;
        }

        static Lesson[] InputGroupLessons(int LessonsAm, StreamReader rdr)
        {
            Lesson[] Timetable = new Lesson[LessonsAm];
            Lesson OneLesson;
            
            for(int i = 0; i < LessonsAm; i++)
            {
                OneLesson = InputLesson(rdr);
                if (Timetable[OneLesson.number - 1].group == null)
                {
                    Timetable[OneLesson.number - 1] = OneLesson;
                }
                else Console.WriteLine($"У класса {OneLesson.group} больше одного {OneLesson.number} урока");
                Console.WriteLine($"{OneLesson.number} урок класса {OneLesson.group} введён");
                
            }
            return Timetable;
        }

        static Lesson[,] CreateTimetable(int GroupsAm, int LessonsAm, StreamReader rdr)
        {
            Lesson[,] Timetable = new Lesson[GroupsAm, LessonsAm];
            for (int i = 0; i < GroupsAm; i++)
            {
                Lesson[] OneGroupTmt = InputGroupLessons(LessonsAm, rdr);
                for (int j = 0; j < LessonsAm; j++) Timetable[i, j] = OneGroupTmt[j];
            }
            return Timetable;
        }



        //task 2
        static void CheckForErrors(Lesson[,] Timetable)
        {
            for (int i = 0; i < Timetable.GetLength(1); i++)
            {
                for (int j = 0; j < Timetable.GetLength(0); j++)
                {
                    for (int k = j + 1; k < Timetable.GetLength(0); k++)
                    {
                        if (Timetable[i, j].classroom == Timetable[i, k].classroom)
                        {
                            Console.WriteLine($"Классы {Timetable[j, 0]} и {Timetable[k, 0]} занимают один и тот же кабинет ({Timetable[i, j]}) на {i} уроке");
                        }
                        if (Timetable[i, j].teacher == Timetable[i, k].teacher)
                        {
                            Console.WriteLine($"У классов {Timetable[j, 0]} и {Timetable[k, 0]} один и тот же учитель ({Timetable[i, j]}) на {i} уроке");
                        }
                    }
                }
            }
        }



        //task 4
        static void PrintByGroup(Lesson[,] Timetable)
        {
            Console.WriteLine("Введите номер группы");
            string group = Console.ReadLine();
            bool wasThere = false;
            for (int i = 0; i < Timetable.GetLength(0); i++)
            {
                if (Timetable[i, 0].group == group)
                {
                    wasThere = true;
                    for (int j = 1; j < Timetable.GetLength(1); j++)
                    {
                        Console.WriteLine(Timetable[i, j].classroom);
                        Console.WriteLine(Timetable[i, j].subject);
                        Console.WriteLine(Timetable[i, j].teacher);
                        Console.WriteLine();
                    }
                }
            }
            if (!wasThere) Console.WriteLine("Группа не найдена");
        }

        static void PrintByLesson(Lesson[,] Timetable)
        {
            Console.WriteLine("Введите номер урока");
            int lessonNum = int.Parse(Console.ReadLine());
            if (lessonNum <= Timetable.GetLength(1))
            {
                for (int i = 0; i < Timetable.GetLength(0); i++)
                {
                    Console.WriteLine(Timetable[i, lessonNum - 1].classroom);
                    Console.WriteLine(Timetable[i, lessonNum - 1].subject);
                    Console.WriteLine(Timetable[i, lessonNum - 1].teacher);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Урок не найден");
            }
        }



        //task 5
        static bool isInArr(string[] arr, string element)
        {
            bool isThere = false;
            foreach (string arrEl in arr)
            {
                if (arrEl == element)
                {
                    isThere = true;
                    break;
                }
            }
            return isThere;
        }

        static string[] SearchForFree(Lesson[,] timetable, int group, int GapLesNum)
        {
            string[] tkdTeachers = new string[timetable.GetLength(0) - 1];
            int count = 0;
            for (int i = 0; i < timetable.GetLength(0); i++)
            {
                if (i != group)
                {
                    count++;
                    tkdTeachers[count] = timetable[i, GapLesNum].teacher;
                }

            }

            string[] tkdCabinets = new string[timetable.GetLength(0) - 1];
            count = 0;
            for (int i = 0; i < timetable.GetLength(0); i++)
            {
                if (i != group)
                {
                    count++;
                    tkdCabinets[count] = timetable[i, GapLesNum].classroom;
                }
            }
            string[] ansArr = { "null, null" };
            for (int i = 0; i < timetable.GetLength(0); i++)
            {
                for (int j = 0; j < timetable.GetLength(1); j++)
                {
                    if (!isInArr(tkdCabinets, timetable[i, j].classroom)) ansArr[0] = timetable[i, j].classroom;
                    if (!isInArr(tkdTeachers, timetable[i, j].teacher)) ansArr[1] = timetable[i, j].teacher;
                    if (ansArr[0] != "null" && ansArr[1] != "null") break;
                }
                if (ansArr[0] != "null" && ansArr[1] != "null") break;
            }
            return ansArr;
        }

        static Lesson[,] FillGaps(Lesson[,] timetable)
        {
            Console.WriteLine("Введите номер класса");
            string group = Console.ReadLine();
            int groupNum = -1;
            for (int i = 0; i < timetable.GetLength(0); i++)
            {
                if (timetable[i, 0].group == group) groupNum = i;
            }
            if (groupNum != -1)
            {
                for(int i = 1; i < timetable.GetLength(0); i += 3)
                {
                    if(timetable[groupNum, i].teacher == "")
                    {
                        string[] Zams = SearchForFree(timetable, groupNum, (i - 1) / 3 + 1);
                        timetable[groupNum, i].classroom = Zams[0];
                        timetable[groupNum, i].teacher = Zams[1];
                    }

                }
            }
            else
            {
                Console.WriteLine("Группа не найдена");
            }
            return timetable;
        }
        //task 6
        static Lesson[,] SwapGroups(Lesson[,] timetable, int GroupNum)
        {
            Lesson helpVar;//i'm really sorry for that
            for (int i = 0; i < timetable.GetLength(1); i++)
            {
                helpVar = timetable[GroupNum, i];
                timetable[GroupNum, i] = timetable[GroupNum + 1, i];
                timetable[GroupNum + 1, i] = helpVar;
            }
            return timetable;
        }


        static Lesson[,] SortTimetable(Lesson[,] timetable)
        {

            for (int i = 0; i < timetable.GetLength(0); i++)
            {
                for (int j = 0; j < timetable.GetLength(0) - 1; j++)
                {
                    if (double.Parse(timetable[j, 0].group) > double.Parse(timetable[j + 1, 0].group))
                    {
                        SwapGroups(timetable, j);
                    }
                }
            }
            return timetable;
        }

        static string[,] CreateFinalTmt(Lesson[,] timetable)
        {
            SortTimetable(timetable);
            string[,] fTimetable = new string[timetable.GetLength(0) + 1, timetable.GetLength(1) * 3 + 1];
            fTimetable[0, 0] = "Урок";
            for (int i = 1; i < timetable.GetLength(1); i += 3)
            {
                fTimetable[0, i] = Convert.ToString(i / 3 + 1);
            }
            for(int i = 1; i < timetable.GetLength(0); i++)
            {
                fTimetable[i, 0] = timetable[i - 1, 0].group;
            }

            for (int i = 1; i < fTimetable.GetLength(0); i++)
            {
                for (int j = 0; j < fTimetable.GetLength(1); j++)
                {
                    fTimetable[i, j * 3 + 1] = timetable[i - 1, j].group;
                    fTimetable[i, j * 3 + 2] = timetable[i - 1, j].subject;
                    fTimetable[i, j * 3 + 3] = timetable[i - 1, j].teacher;
                }
            }
            return fTimetable;
        }

        static void PrintTimetable(Lesson[,] timetable)
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
            StreamReader rdr = new StreamReader(@"/Users/K0pyt0/Documents/LIT/OOP/SupportFiles/Bcgr42_Input.txt");
            int GroupsAmount = int.Parse(rdr.ReadLine());
            int LessonsAmount = int.Parse(rdr.ReadLine());
            Lesson[,] timetable = CreateTimetable(GroupsAmount, LessonsAmount, rdr);
            PrintTimetable(timetable);
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
            PrintTimetable(FillGaps(timetable));
            
        }
    }
}
