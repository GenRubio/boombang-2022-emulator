using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Connection
{
    public class Encryption
    {
        public readonly Encoding Encoding;
        public readonly Encoding DefaultEncoding;
        private int DecipherConstant;
        private int DecipherMorph;
        private int EncipherConstant;
        private int EncipherMorph;
        public Encryption()
        {
            Encoding = Encoding.GetEncoding("iso-8859-1");
            DefaultEncoding = Encoding.Default;
            DecipherConstant = 135;
            EncipherConstant = 135;
        }
        public  byte[] Decrypt(byte[] Data)
        {
            int Length = Data.Length;
            int ActualByte;
            int Morph;
            byte[] Buffer = new byte[Length];
            int Index = 0;
            while (Length-- > 0)
            {
                ActualByte = Data[Index];
                Morph = (ActualByte ^ DecipherConstant) ^ DecipherMorph;
                Buffer[Index] = (byte)Morph;
                Index++;
                DecipherConstant = Rand(DecipherConstant);
                DecipherMorph = Morph;
            }
            return Buffer;
        }
        public  byte[] Encrypt(byte[] Data)
        {
            int Length = Data.Length;
            int ActualByte;
            int Morph;
            byte[] Buffer = new byte[Length];
            int Index = 0;
            while (Length-- > 0)
            {
                ActualByte = Data[Index];
                Morph = (ActualByte ^ EncipherConstant) ^ EncipherMorph;
                Buffer[Index] = (byte)Morph;
                Index++;
                EncipherConstant = Rand(EncipherConstant);
                EncipherMorph = ActualByte;
            }
            return Buffer;
        }
        private int Rand(int n)
        {
            return ((1103515245 * n) + 12344);
        }
    }
}
