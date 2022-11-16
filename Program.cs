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
        static byte[] data, temp_data;
        public static BitArray bitArray;
        static int[] sumVertical;
        
        private static void GetMsgfromFile(bool isMsg = true)
        {
            string filePath = Directory.GetCurrentDirectory() + @"\FileSource.txt";
            if (!isMsg) filePath = Directory.GetCurrentDirectory() + @"\Polynom.txt";
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
            string text = File.ReadAllText(filePath, Encoding.GetEncoding(1251));
            Console.WriteLine(text);
            data = Encoding.GetEncoding(866).GetBytes(text);
            Array.Reverse(data);
        }

        static void ParityBit(BitArray array, bool HVArray = false)
        {
            int count = 0, sum = 0;
            sumVertical = new int[8];
            foreach (var bit in array)
            {
                int x = Convert.ToInt32(bit);
                if (x == 1)
                {
                    sum++;
                    sumVertical[count]++;
                }
                count++;
                Console.Write(x);
                if (count == 8)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" " + sum % 2 + " ");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    sum = 0;
                    count = 0;
                }
            }
            if (HVArray)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                for (int i = 0; i < 8; i++)
                {
                    Console.Write(sumVertical[i] % 2);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("- - - - - - - - - - - - -");
            }
        }

        public static void Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);
            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }
        
        private static void CyclicRedundancyCheck(BitArray bitArray)
        {
            
        }

        public static void PrintArr(BitArray bitArray)
        {
            {
                foreach (var bit in bitArray)
                {
                    Console.Write(Convert.ToInt32(bit));
                }
                Console.WriteLine();
                Console.WriteLine("- - - - - - - - - - - - -");
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Исходное сообщение:");
            GetMsgfromFile();
            bitArray = new BitArray(data);
            Reverse(bitArray);
            Console.WriteLine("Сообщение в двоичном коде:");
            PrintArr(bitArray);
            Console.WriteLine("Контроль по паритету: ");
            ParityBit(bitArray);
            Console.WriteLine("- - - - - - - - - - - - -");
            Console.WriteLine("Вертикальный и горизонтальный контроль: ");
            ParityBit(bitArray, true);
            Console.WriteLine("Циклический избыточный контроль: ");
            ClassCRC.MakeCRC();
            Console.WriteLine("Полином:");
            GetMsgfromFile(false);
            Console.WriteLine("Полином в двоичном виде:");
            PrintArr(ClassCRC.polynom);
            Console.WriteLine("Контрольная сумма:");
            PrintArr(ClassCRC.register);
        }
    }
}
