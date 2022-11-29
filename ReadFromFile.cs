using ControlSum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ReadFromFile
    {
        public enum ACTIONS
        {
            READ_FROM_FILE,
            CONVERT_TO_BIT_ARRAY,
            PARITY_BIT,
            PARITY_HV,
            CRC
        }

        public static byte[] data;

        public void SetPolynomFromFile()
        {
            string filePath = Directory.GetCurrentDirectory() + @"\Polynom.txt";
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
            string text = File.ReadAllText(filePath, Encoding.GetEncoding(1251));
            Console.WriteLine(text);
            data = Encoding.GetEncoding(866).GetBytes(text);
            Array.Reverse(data);
        }

            public void ReadF(ACTIONS action) {
            var fileInfo = new FileInfo("FileSource.txt");
            int chunkSize = 9;
            byte[] bytes = new byte[chunkSize];
            using (var stream = fileInfo.OpenRead())
            {
                int totalBytes = (int)stream.Length;
                int bytesRead = 0;
                Console.ForegroundColor = ConsoleColor.White;
                while (bytesRead < totalBytes)
                {
                    int count = stream.Read(bytes, 0, bytes.Length);
                    EncodingProvider provider = CodePagesEncodingProvider.Instance;
                    Encoding.RegisterProvider(provider);
                    data = bytes;
                    if (action != ACTIONS.READ_FROM_FILE)
                    Array.Reverse(data);
                    bytesRead += count;
                    BitArray bitArray = new BitArray(data);
                  //  Algorithms.Reverse(bitArray);
                    switch (action)
                    {
                        case ACTIONS.READ_FROM_FILE:
                            foreach (char c in data)
                            {
                                Console.Write(c);
                            }
                            Console.WriteLine();
                            break;
                        case ACTIONS.CONVERT_TO_BIT_ARRAY:
                            Algorithms.PrintArr(bitArray);
                            break;
                        case ACTIONS.PARITY_BIT:
                            Algorithms.ParityBit(bitArray);
                            break;
                        case ACTIONS.PARITY_HV:
                            Algorithms.ParityBit(bitArray, true);
                            break;
                        case ACTIONS.CRC:
                            Algorithms.MakeCRC(bitArray);
                            Console.WriteLine("Контрольная сумма:");
                            Algorithms.PrintArr(Algorithms.register);
                            break;
                    }
                }
            }
        }
    }
}
