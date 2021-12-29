using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Pathfinding.A_Star
{
    public class SearchParameters
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public bool[,] Map { get; set; }
        public Session Session;
        public Area Sala;
        public SearchParameters(Point endLocation, Session Session)
        {
            this.Sala = Session.User.Area;
            this.StartLocation = new Point(Session.User.Posicion.x, Session.User.Posicion.y);
            this.EndLocation = endLocation;
            this.Map = Sala.MapaBytes.BoolMap;
        }
    }
}
