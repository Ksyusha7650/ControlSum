using ControlSum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ClassCRC
    {
        const int DEGREE = 30;
        public static BitArray bitArray = new BitArray(DEGREE + Program.bitArray.Length),
            register = new BitArray(DEGREE), polynom = new BitArray(DEGREE);


        private static void SetSizeToBitArray()
        {
            for (int i = 0; i < Program.bitArray.Length; i++)
            {
                bitArray[i] = Program.bitArray[i];
            }
            for(int i = 0; i < DEGREE; i++)
            {
                if ((i == 30) || (i == 29) || (i == 21) || (i == 20) || (i == 15) ||
                    (i == 13) || (i == 12) || (i == 11) || (i == 8) || (i == 7) ||
                    (i == 6) || (i == 2) || (i == 1) || (i == 0)) polynom[i] = true;
            }
            Program.Reverse(polynom);
        }

        public static void MakeCRC()
        {
            SetSizeToBitArray();
            foreach(var bit in bitArray)
            {
                var b = register[0];
                for (int i = 1; i < DEGREE; i++)
                {
                    register[i - 1] = register[i];
                }
                register[DEGREE - 1] = (bool)bit;
                if (b == true) register = register.Xor(polynom);   
            }
        }

    }
}
