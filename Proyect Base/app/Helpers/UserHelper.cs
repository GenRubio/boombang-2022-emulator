using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        public static string makeSessionUID()
        {
            return uidMaker(Convert.ToSingle(DateTime.Now.ToOADate()).ToString(), new Random().Next(11111, 99999).ToString());
        }
        private static string uidMaker(string clearText, string key)
        {
            string EncryptionKey = key;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
