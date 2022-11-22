using ControlSum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Algorithms
    {
        const int DEGREE = 29;
        public static BitArray bitArrayCRC,
            register = new BitArray(DEGREE), polynom = new BitArray(DEGREE), toXorPolynom = new BitArray(DEGREE);

        public static byte[] data, bytesPolynom;
        public static BitArray bitArray;
        static int[] sumVertical;

        private static void SetSizeToBitArray()
        {
            bitArrayCRC = new BitArray(DEGREE + bitArray.Length);
            for (int i = 0; i < bitArray.Length; i++)
            {
                bitArrayCRC[i] = bitArray[i];
            }
           
            for (int i = 0; i < DEGREE; i++)
            {
                if ((i == 21) || (i == 20) || (i == 15) ||
                    (i == 13) || (i == 12) || (i == 11) || (i == 8) || (i == 7) ||
                    (i == 6) || (i == 2) || (i == 1) || (i == 0)) polynom[i] = true;
                register[i] = toXorPolynom[i] = true;
            }
            
           Algorithms.Reverse(polynom);
            
        }

        public static void MakeCRC(BitArray bits)
        {
            bitArray = bits;
            SetSizeToBitArray();
            foreach (var bit in bitArrayCRC)
            {
                var b = register[0];
                register.RightShift(1);
                register[DEGREE - 1] = (bool)bit;
                if (b == true) register = register.Xor(polynom);
            }
            register = register.Xor(toXorPolynom);
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

        public static void ParityBit(BitArray array, bool HVArray = false)
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

    }
}
