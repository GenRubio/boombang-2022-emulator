using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Helpers
{
    class CharactersHelper
    {
        public static bool validTextNormal(string text)
        {
            string[] Permitidos = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "ñ", "z", "x", "c", "v", "b", "n", "m", ",", ".", "-", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M", "@", "!", "=", ":", ".", ",", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int id = 0; id < text.Length; id++)
            {
                if (!Permitidos.Contains(text[id].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool validTextExtend(string text)
        {
            string[] Permitidos = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "ñ", "z", "x", "c", "v", "b", "n", "m", ",", ".", "-", "_", "á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ñ", "Z", "X", "C", "V", "B", "N", "M", "{", "}", "[", "]", "@", "/", "-", "+", " ", "*", "'", "!", "#", "$", "%", "&", "(", ")", "=", "?", "¿", "¡", ":", ";", "<", ">", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int id = 0; id < text.Length; id++)
            {
                if (!Permitidos.Contains(text[id].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
