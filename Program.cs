using System;
using System.Threading;

namespace массивы
{
    class Program
    {
        static void ClearingLine(int x, int y)
        {
            Console.SetCursorPosition(Console.BufferWidth-1,y);
            for (int i = Console.BufferWidth-1; i > x; i--)
            {
                Console.Write('\b'+" "+'\b');
            }
        }
        static bool IsContains(int[][] array1, int[][] array2)
        {
            int counter = 0;
            int maxequals =0;
            int win = 0;
            bool stopcheckingline = false;
            for (int a = 0; a < array2.Length; a++)
            {
                win += array2[a].Length;

                for (int i = 0; i < array1.Length; i++)
                {

                    for (int j = 0; j <= array1[i].Length - array2[a].Length; j++) // до разности длин +1 есть смысл искать, далее нет
                    {
                        if (array1[i][j] == array2[a][0])

                            for (int b = 0; b < array2[a].Length; b++)
                                if (array1[i][j + b] == array2[a][b])
                                {
                                    counter += 1;
                                    if (counter == array2[a].Length)        //Если уже нашли подходящие числа в массиве
                                    {
                                        maxequals += counter;
                                        counter = 0;
                                        stopcheckingline = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    counter = 0;
                                }
                        if (stopcheckingline)
                            break;

                    }

                    if (stopcheckingline)
                    {
                        stopcheckingline = false;
                        break;
                    }
                }
            }

            if (maxequals == win) 
                return true;
            else 
                return false;
        }

        static int[][] FillingAndPrintingArray()  //Заполнение и печатание массива
        {
            int x1 = Console.CursorLeft;
            int y1 = Console.CursorTop;

            PrintingColoredString("Введите количество внешних массивов\n","green");
            int numout = ReadCount(true);

            ClearingLine(x1,y1);

            int[][] array = new int[numout][];

            //Console.WriteLine();

            for (int a = 0; a < numout; a++)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;

                PrintingColoredString("\nВведите количество хранящихся в этом массиве чисел\n", "green");
                int numin = ReadCount(true);

                ClearingLine(0,y+1);

                Console.SetCursorPosition(x, y);

                array[a] = new int[numin];

                Console.Write("[");        //Рисовка внутреннего массива
                for (int j = 0; j < array[a].Length; j++)
                {
                    array[a][j] = ReadCount(false);
                    Console.Write(array[a][j]);
                    Console.Write(",");
                }
                Console.Write('\b');
                Console.Write("]");
            }

            Console.WriteLine();

            return array;
        }

        static int ReadCount(bool morethanzero)
        {
            int count;
            bool checkparse;

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            do
            {
                checkparse = int.TryParse(Console.ReadLine(), out count);
                if (!checkparse)
                {
                    PrintingColoredString("Вы неправильно ввели число","red");
                    Thread.Sleep(1000);
                    ClearingLine(0,y+1);
                    ClearingLine(x,y);
                }
                else if (morethanzero && count <= 0)
                {
                    checkparse = false;
                    PrintingColoredString("Число должно быть больше нуля","red");
                    Thread.Sleep(1000);
                    ClearingLine(0,y+1);
                    ClearingLine(x,y);
                }
            } while (!checkparse);

            ClearingLine(x,y);

            return count;
        }

        static void PrintingColoredString(string str, string color)
        {
            switch(color)
            {
                case "red":
                    Console.ForegroundColor=ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.Write(str);
            Console.ForegroundColor=ConsoleColor.White;
        }

        static void Main()
        {
            Console.WriteLine("Первый массив");
            int [][]array1 = FillingAndPrintingArray();

            Console.WriteLine("\nВторой массив");
            int[][] array2 = FillingAndPrintingArray();

            Console.WriteLine();

            if(IsContains(array1, array2))
                PrintingColoredString("Да","green");
            else
                PrintingColoredString("Нет :(","red");

            Console.ReadKey();
        }
    }
}