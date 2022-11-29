using ControlSum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Algorithms
    {
        const int DEGREE = 30;
        public static BitArray bitArrayCRC,
            register = new BitArray(DEGREE), polynom = new BitArray(DEGREE), toXorPolynom = new BitArray(DEGREE);
        private const uint polynomHex = 0x2030B9C7, registerHex = 0x3FFFFFFF, toXorPolynomHex = 0x3FFFFFFF;
        public static BitArray bitArray;
        static int[] sumVertical;

        private static void SetSizeToBitArray()
        {
            Reverse(bitArray);
            ClearFirstZeros(ref bitArray);
            bitArrayCRC = new BitArray(DEGREE + bitArray.Length);
            for (int i = 0; i < bitArray.Length; i++)
            {
                bitArrayCRC[i] = bitArray[i];
            }
            polynom = new BitArray(BitConverter.GetBytes(polynomHex));
            ClearFirstZeros(ref polynom);
            Reverse(polynom);
            register = new BitArray(BitConverter.GetBytes(registerHex));
            ClearFirstZeros(ref register);
            toXorPolynom = new BitArray(BitConverter.GetBytes(toXorPolynomHex));
            ClearFirstZeros(ref toXorPolynom);
        }

        private static void ClearFirstZeros(ref BitArray bitArray)
        {
            while (bitArray[bitArray.Length - 1] == false)
            {
                bitArray.Length -= 1;
            }
        }

        public static void MakeCRC(byte[] bytes)
        {
            bitArray = new BitArray(bytes);
            SetSizeToBitArray();
            BitArray temp = new BitArray(bytes);
            Reverse(temp);
            temp.Length = DEGREE;
            register = register.Xor(temp);
            bitArrayCRC.RightShift(DEGREE);
            bitArrayCRC.Length -= DEGREE;
            foreach (var bit in bitArrayCRC)
            {
                var b = register[0];
                register.RightShift(1);
                register[DEGREE - 1] = (bool)bit;
                if (b == true) register = register.Xor(polynom);
            }
            register = register.Xor(toXorPolynom);
            Reverse(register);
            ClearFirstZeros(ref register);
            Reverse(register);
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
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var bit in bitArray)
            {
                 Console.Write(Convert.ToInt32(bit));
            }
            Console.WriteLine();
        }

        public static string SetStringOfBits(BitArray bitArray)
        {
            StringBuilder sb = new StringBuilder(bitArray.Length);
            foreach (var bit in bitArray)
            {
                sb.Append(Convert.ToInt32(bit));
            }
            return sb.ToString();
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
