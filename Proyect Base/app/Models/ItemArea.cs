using Proyect_Base.app.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class ItemArea
    {
        public int id { get; private set; }
        public string nombre { get; set; }
        public int modelo { get; set; }
        public int concurso_id { get; set; }
        public int id_lanzamiento { get; set; }
        public int tipo_salida { get; set; }
        public int tipo_caida { get; set; }
        public int tiempo_aparicion { get; set; }
        public int tiempo_desaparicion { get; set; } //In seconds
        public ItemArea(DataRow row)
        {
            this.id = (int)row["id"];
            this.nombre = (string)row["nombre"];
            this.modelo = (int)row["modelo"];
            this.concurso_id = (int)row["concurso_id"];
            this.tipo_salida = (int)row["tipo_salida"];
            this.tipo_caida = (int)row["tipo_caida"];
            this.tiempo_aparicion = (int)row["tiempo"];
            this.tiempo_desaparicion = 15;
        }
    }
}
