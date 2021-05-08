﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab_4
{
    class LFSR
    {
        private const int BUFFER_SIZE = 1024;
        private const int BITS_NUMBER = 8;
        public static ulong register;

        public static void Crypt(string srcPath, string destPath)
        {
            BinaryReader reader = new BinaryReader(File.Open(srcPath, FileMode.Open));
            BinaryWriter writer = new BinaryWriter(File.Open(destPath, FileMode.Create));

            byte[] bufData = new byte[BUFFER_SIZE];
            int readBytes;

            try
            {
                do
                {
                    readBytes = reader.Read(bufData, 0, BUFFER_SIZE);

                    for (int i = 0; i < BUFFER_SIZE; i++)
                    {
                        for (int j = 0; j < BITS_NUMBER; j++)
                        {
                            bufData[i] = (byte)(bufData[i] ^ (Key() << (8 - j)));
                        }
                    }

                    writer.Write(bufData, 0, readBytes);
                } while (readBytes > 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
                writer.Close();
            }
        }

        // x^29 + x^2 + 1
        public static ulong Key()
        {
            register = ((((register >> 0) ^ (register >> 2) ^ (register >> 29)) & 1) << 31) | (register >> 1);
            return register & 1;
        }
    }
}
