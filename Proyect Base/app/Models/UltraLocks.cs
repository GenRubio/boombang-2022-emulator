using Proyect_Base.app.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public enum UltraType
    {
        Mirada, Acciones, Uppercut, Coco
    }
    public class UltraLocks
    {
        //Coco
        public int Coco_Time = 0;
        private int Coco_LastID = 0;
        private double Coco_LastTime;
        //Uppercut
        public int Uppert_Time = 0;
        private int Uppert_LastID = 0;
        private double Uppert_LastTime;
        //Mirada
        private int Mirada_LastID = 0;
        private double Mirada_LastTime;
        //Acciones
        private int Acciones_LastID = 0;
        private double Acciones_LastTime;
        public void SetLock(UltraType Type)
        {
            switch (Type)
            {
                case UltraType.Mirada: Mirada_LastTime = TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 350); break;
                case UltraType.Acciones: Acciones_LastTime = TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 250); break;
                case UltraType.Uppercut: Uppert_LastTime = TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 400); break;
                case UltraType.Coco: Coco_LastTime = TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 600); break;
            }
        }
        public bool IsBlock(UltraType Type)
        {
            switch (Type)
            {
                case UltraType.Mirada: if (this.Mirada_LastTime > TimeHelper.TiempoActual()) return true; return false;
                case UltraType.Acciones: if (this.Acciones_LastTime > TimeHelper.TiempoActual()) return true; return false;
                case UltraType.Uppercut: if (this.Uppert_LastTime > TimeHelper.TiempoActual()) return true; return false;
                case UltraType.Coco: if (this.Coco_LastTime > TimeHelper.TiempoActual()) return true; return false;
                default: return false;
            }
        }
        public void Verificar(UltraType Type, int Valor)
        {
            switch (Type)
            {
                case UltraType.Mirada: if (Mirada_LastID != Valor) SetLock(UltraType.Mirada); Mirada_LastID = Valor; break;
                case UltraType.Acciones: if (Acciones_LastID != Valor) SetLock(UltraType.Acciones); Acciones_LastID = Valor; break;
                case UltraType.Uppercut: if (Uppert_LastID != Valor) SetLock(UltraType.Uppercut); Uppert_LastID = Valor; break;
                case UltraType.Coco: if (Coco_LastID != Valor) SetLock(UltraType.Uppercut); Coco_LastID = Valor; break;
            }
        }
    }
}
