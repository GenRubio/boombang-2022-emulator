using Proyect_Base.app.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public enum Bloqueo
    {
        Chat, Ficha, Acciones, Acciones_Especiales, Mirada, Block, Caminando, Ficha_Texto, Disfraces_Gorros
    }
    public class PreLocks
    {
        private double Chat;
        private double Ficha;
        private double Acciones;
        private double Acciones_Especiales;
        private double Mirada;
        private double Block;
        private double Caminando;
        private double Ficha_Texto;
        private double Disfraces_Gorros;
        public void SetLock(Bloqueo Type, double Tiempo)
        {
            switch (Type)
            {
                case Bloqueo.Chat: this.Chat = Tiempo; break;
                case Bloqueo.Ficha: this.Ficha = Tiempo; break;
                case Bloqueo.Acciones: this.Acciones = Tiempo; break;
                case Bloqueo.Acciones_Especiales: this.Acciones_Especiales = Tiempo; break;
                case Bloqueo.Mirada: this.Mirada = Tiempo; break;
                case Bloqueo.Block: this.Block = Tiempo; break;
                case Bloqueo.Caminando: this.Caminando = Tiempo; break;
                case Bloqueo.Ficha_Texto: this.Ficha_Texto = Tiempo; break;
                case Bloqueo.Disfraces_Gorros: this.Disfraces_Gorros = Tiempo; break;
            }
        }
        public bool IsBlock(Bloqueo Type)
        {
            switch (Type)
            {
                case Bloqueo.Chat: if (this.Chat > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Ficha: if (this.Ficha > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Acciones: if (this.Acciones > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Acciones_Especiales: if (this.Acciones_Especiales > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Mirada: if (this.Mirada > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Block: if (this.Block > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Caminando: if (this.Caminando > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Ficha_Texto: if (this.Ficha_Texto > TimeHelper.TiempoActual()) return true; return false;
                case Bloqueo.Disfraces_Gorros: if (this.Disfraces_Gorros > TimeHelper.TiempoActual()) return true; return false;
                default: return false;
            }
        }
    }
}
