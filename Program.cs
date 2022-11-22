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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Исходное сообщение:");
            ReadFromFile r = new ReadFromFile();
            r.ReadF(ReadFromFile.ACTIONS.READ_FROM_FILE);
            Console.WriteLine("Сообщение в двоичном коде:");
            r.ReadF(ReadFromFile.ACTIONS.CONVERT_TO_BIT_ARRAY);
            Console.WriteLine("Контроль по паритету: ");
            r.ReadF(ReadFromFile.ACTIONS.PARITY_BIT);
            Console.WriteLine("Вертикальный и горизонтальный контроль: ");
            r.ReadF(ReadFromFile.ACTIONS.PARITY_HV);
            Console.WriteLine("Полином: ");
          //  r.SetPolynomFromFile();
            Console.WriteLine("Циклический избыточный контроль: ");
            r.ReadF(ReadFromFile.ACTIONS.CRC);
        }
    }
}
