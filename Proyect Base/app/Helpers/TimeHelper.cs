using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Helpers
{
    public enum AddType { Milisegundos, Segundos, Minutos, Horas, Dias, Meses, Años }
    class TimeHelper
    {
        public static DateTime getDate(string date)
        {
            if (date != string.Empty)
            {
                return DateTime.Parse(date);
            }
            return DateTime.MinValue;
        }
        public static double GetCurrentAndAdd(AddType Type, int Valor)
        {
            DateTime Test;
            switch (Type)
            {
                case AddType.Milisegundos:
                    Test = DateTime.Now.AddMilliseconds(Valor);
                    break;
                case AddType.Segundos:
                    Test = DateTime.Now.AddSeconds(Valor);
                    break;
                case AddType.Minutos:
                    Test = DateTime.Now.AddMinutes(Valor);
                    break;
                case AddType.Horas:
                    Test = DateTime.Now.AddHours(Valor);
                    break;
                case AddType.Dias:
                    Test = DateTime.Now.AddDays(Valor);
                    break;
                case AddType.Meses:
                    Test = DateTime.Now.AddMonths(Valor);
                    break;
                case AddType.Años:
                    Test = DateTime.Now.AddYears(Valor);
                    break;
                default:
                    Test = DateTime.Now.AddSeconds(Valor);
                    break;
            }
            TimeSpan span = (TimeSpan)(Test - new DateTime(0x7b2, 1, 1, 0, 0, 0));
            return span.TotalSeconds;
        }
        public static int GetDifference(double Hora_Fin)
        {
            return Convert.ToInt32(Hora_Fin - TiempoActual());
        }
        public static bool Bloqueado(double Hora_Fin)
        {
            if (Hora_Fin > TiempoActual()) return true;
            return false;
        }
        public static double TiempoActual()
        {
            DateTime Test = DateTime.Now;
            TimeSpan span = (TimeSpan)(Test - new DateTime(0x7b2, 1, 1, 0, 0, 0));
            return span.TotalSeconds;
        }
    }
}
