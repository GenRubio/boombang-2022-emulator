using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proyect_Base.app.Connection
{
    public class ClientMessage
    {
        private string Data;
        public readonly List<int> Header = new List<int>();
        public readonly string[,] Parameters;
        public ClientMessage(string Data)
        {
            try
            {
                this.Data = Data;
                foreach (string ActualHeader in Regex.Split(Regex.Split(Data.Substring(1), "³²")[0], "³"))
                {
                    if (ActualHeader.Length == 1)
                    {
                        this.Header.Add(Convert.ToByte(Convert.ToChar(ActualHeader)));
                    }
                    else
                    {
                        this.Header.Add((byte)0);
                    }
                }

                int SubParametersLength = 0;
                for (int Pointer1 = 1; Pointer1 < Data.Split('²').Length; Pointer1++)
                {
                    for (int Pointer2 = 0; Pointer2 < Data.Split('²')[Pointer1].Split('³').Length - 1; Pointer2++)
                    {
                        if (Data.Split('²')[Pointer1].Split('³').Length > SubParametersLength)
                        {
                            SubParametersLength = Data.Split('²')[Pointer1].Split('³').Length;
                        }
                    }
                }

                this.Parameters = new string[Data.Split('²').Length, SubParametersLength];
                for (int Pointer1 = 1; Pointer1 < Data.Split('²').Length; Pointer1++)
                {
                    for (int Pointer2 = 0; Pointer2 < Data.Split('²')[Pointer1].Split('³').Length - 1; Pointer2++)
                    {
                        this.Parameters[Pointer1 - 1, Pointer2] = Data.Split('²')[Pointer1].Split('³')[Pointer2];
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public string GetData()
        {
            return this.Data;
        }
        public string GetHandler()
        {
            string Handler = "Method";
            foreach (int ActualHeader in Header)
            {
                Handler += "_" + ActualHeader;
            }
            return Handler;
        }
        public int GetInteger()
        {
            string num = "";
            foreach (int ActualHeader in Header)
            {
                num += ActualHeader;
            }
            return Convert.ToInt32(num);
        }
        public new string ToString()
        {
            return "Packet Information: " + this.GetHandler() + " -> " + this.GetData();
        }
    }
}
