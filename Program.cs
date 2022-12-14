using ConsoleApp1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ControlSum
{
    internal class Program
    {

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Что будем делать?:");
                Console.WriteLine("1. Чтение из файла");
                Console.WriteLine("2. Вывод собщения в двоичном виде");
                Console.WriteLine("3. Контроль по паритету");
                Console.WriteLine("4. Контроль по горизонтальной и вертикальной сумме");
                Console.WriteLine("5. Контроль по CRC");
                Console.WriteLine("6. Выход");
                Console.Write("Ваш выбор: ");
                int choice = 0;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Неверный ввод");
                }
                ReadFromFile r = new ReadFromFile();
                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("Исходное сообщение:");
                        r.ReadF(ReadFromFile.ACTIONS.READ_FROM_FILE);
                        break;
                    case 2:
                        Console.WriteLine("Сообщение в двоичном коде:");
                        r.ReadF(ReadFromFile.ACTIONS.CONVERT_TO_BIT_ARRAY);
                        break;
                    case 3:
                        Console.WriteLine("Контроль по паритету: ");
                        r.ReadF(ReadFromFile.ACTIONS.PARITY_BIT);
                        break;
                    case 4:
                        Console.WriteLine("Вертикальный и горизонтальный контроль: ");
                        r.ReadF(ReadFromFile.ACTIONS.PARITY_HV);
                        break;
                    case 5:
                        Console.WriteLine("Циклический избыточный контроль: ");
                        Console.WriteLine("Полином: ");
                        Algorithms.PrintUint(Algorithms.polynomHex);
                        Console.WriteLine("Значение регистра: ");
                        Algorithms.PrintUint(Algorithms.registerHex);
                        Console.WriteLine("Полином 2: ");
                        Algorithms.PrintUint(Algorithms.toXorPolynomHex);
                        r.ReadF(ReadFromFile.ACTIONS.CRC);
                        break;
                    case 6:
                        exit = true;
                        Console.WriteLine("До свидания!");
                        break;
                    default: Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
    }
}
