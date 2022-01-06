using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Helpers
{
    class UserHelper
    {
        public static bool userOnline(User User)
        {
            if (SessionCollection.onlineUsers.ContainsKey(User.id))
            {
                return true;
            }
            return false;
        }
        public static string hashMake(string Password)
        {
            return Password;
            //byte[] Encrypt = hash(Encoding.Default.GetBytes(Password));
            //char[] Chars = Encoding.Default.GetChars(Encrypt);
            //string newString = "";
            //foreach (Char Char in Chars)
            //{
            //    int value = Convert.ToInt32(Char);
            //    newString = newString += value.ToString();
            //}
            //return newString;
        }
        private static byte[] hash(byte[] Data)
        {
            int EncipherConstant = 12337;
            int EncipherMorph = 0;
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
                EncipherConstant = ((EncipherConstant * EncipherConstant));
                EncipherMorph = ActualByte;
            }
            return Buffer;
        }
    }
}
