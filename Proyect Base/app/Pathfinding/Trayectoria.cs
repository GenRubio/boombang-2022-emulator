using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using Proyect_Base.app.Pathfinding.A_Star;
using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Pathfinding
{
    public class Trayectoria
    {
        public List<Posicion> Movimientos = new List<Posicion>();
        private Session Session;
        private AreaNpc areaNpc;
        public Point EndLocation;
        public Trayectoria(Session Session)
        {
            this.Session = Session;
        }
        public Trayectoria(AreaNpc areaNpc)
        {
            this.areaNpc = areaNpc;
        }
        public void AñadirMovimiento(int x, int y, int z)
        {
            Movimientos.Add(new Posicion(x, y, z));
        }
        public int MovimientosTotales()
        {
            return Movimientos.Count;
        }
        public void DetenerMovimiento()
        {
            Movimientos.Clear();
        }
        public void IsMovementCorrupt(Posicion NextPoint)
        {
            try
            {
                if (NextPoint.x == Session.User.Posicion.x && NextPoint.y == Session.User.Posicion.y)
                {
                    this.Movimientos.Remove(this.Movimientos[0]);
                }
                if (Session.User.Posicion.x == NextPoint.x && Session.User.Posicion.y == NextPoint.y)
                {
                    this.Movimientos.Remove(this.Movimientos[0]);
                }
                if (Session.User.Posicion.x + 1 == NextPoint.x && Session.User.Posicion.y + 1 == NextPoint.y && NextPoint.z != 1)
                {
                    NextPoint.z = 1;
                }
                else if (Session.User.Posicion.x - 1 == NextPoint.x && Session.User.Posicion.y - 1 == NextPoint.y && NextPoint.z != 2)
                {
                    NextPoint.z = 2;
                }
                else if (Session.User.Posicion.x + 1 == NextPoint.x && Session.User.Posicion.y - 1 == NextPoint.y && NextPoint.z != 3)
                {
                    NextPoint.z = 3;
                }
                else if (Session.User.Posicion.x - 1 == NextPoint.x && Session.User.Posicion.y + 1 == NextPoint.y && NextPoint.z != 4)
                {
                    NextPoint.z = 4;
                }
                else if (Session.User.Posicion.x + 1 == NextPoint.x && Session.User.Posicion.y == NextPoint.y && NextPoint.z != 5)
                {
                    NextPoint.z = 5;
                }
                else if (Session.User.Posicion.x == NextPoint.x && Session.User.Posicion.y - 1 == NextPoint.y && NextPoint.z != 6)
                {
                    NextPoint.z = 6;
                }
                else if (Session.User.Posicion.x == NextPoint.x && Session.User.Posicion.y + 1 == NextPoint.y && NextPoint.z != 7)
                {
                    NextPoint.z = 7;
                }
                else if (Session.User.Posicion.x - 1 == NextPoint.x && Session.User.Posicion.y == NextPoint.y && NextPoint.z != 8)
                {
                    NextPoint.z = 8;
                }
            }
            catch
            {

            }
        }
        public void IsMovementCorruptNpc(Posicion NextPoint)
        {
            try
            {
                if (NextPoint.x == areaNpc.Posicion.x && NextPoint.y == areaNpc.Posicion.y)
                {
                    this.Movimientos.Remove(this.Movimientos[0]);
                }
                if (areaNpc.Posicion.x == NextPoint.x && areaNpc.Posicion.y == NextPoint.y)
                {
                    this.Movimientos.Remove(this.Movimientos[0]);
                }
                if (areaNpc.Posicion.x + 1 == NextPoint.x && areaNpc.Posicion.y + 1 == NextPoint.y && NextPoint.z != 1)
                {
                    NextPoint.z = 1;
                }
                else if (areaNpc.Posicion.x - 1 == NextPoint.x && areaNpc.Posicion.y - 1 == NextPoint.y && NextPoint.z != 2)
                {
                    NextPoint.z = 2;
                }
                else if (areaNpc.Posicion.x + 1 == NextPoint.x && areaNpc.Posicion.y - 1 == NextPoint.y && NextPoint.z != 3)
                {
                    NextPoint.z = 3;
                }
                else if (areaNpc.Posicion.x - 1 == NextPoint.x && areaNpc.Posicion.y + 1 == NextPoint.y && NextPoint.z != 4)
                {
                    NextPoint.z = 4;
                }
                else if (areaNpc.Posicion.x + 1 == NextPoint.x && areaNpc.Posicion.y == NextPoint.y && NextPoint.z != 5)
                {
                    NextPoint.z = 5;
                }
                else if (areaNpc.Posicion.x == NextPoint.x && areaNpc.Posicion.y - 1 == NextPoint.y && NextPoint.z != 6)
                {
                    NextPoint.z = 6;
                }
                else if (areaNpc.Posicion.x == NextPoint.x && areaNpc.Posicion.y + 1 == NextPoint.y && NextPoint.z != 7)
                {
                    NextPoint.z = 7;
                }
                else if (areaNpc.Posicion.x - 1 == NextPoint.x && areaNpc.Posicion.y == NextPoint.y && NextPoint.z != 8)
                {
                    NextPoint.z = 8;
                }
            }
            catch
            {

            }
        }
        public Posicion SiguienteMovimiento()
        {
            Posicion NextStep = Movimientos[0];
            if (!Session.User.Area.MapaBytes.IsWalkable(NextStep.x, NextStep.y) || Session.User.Area.getSession(NextStep.x, NextStep.y) != null)
            {
                if (Session.User.Area is IslandArea)
                {
                    IslandArea islandArea = (IslandArea)Session.User.Area;
                    if (!islandArea.WalkByObjects(NextStep.x, NextStep.y))
                    {
                        if (Movimientos.Count >= 1) Movimientos.Clear();
                        IniciarCaminado();
                        NextStep = Movimientos[0];
                    }
                }
                else
                {
                    if (Movimientos.Count >= 1) Movimientos.Clear();
                    IniciarCaminado();
                    NextStep = Movimientos[0];
                }
            }
            IsMovementCorrupt(NextStep);
            return NextStep;
        }
        public Posicion SiguienteMovimientoNpc()
        {
            Posicion NextStep = Movimientos[0];
            if (areaNpc.getArea().MapaBytes.IsWalkable(NextStep.x, NextStep.y) == false || areaNpc.getArea().npcOcupedPoint(NextStep.x, NextStep.y))
            {
                Movimientos.Clear();
                return null;
            }
            else if (areaNpc.getArea().getSession(NextStep.x, NextStep.y) != null)
            {
                Movimientos.Clear();
                return null;
            }
            IsMovementCorruptNpc(NextStep);
            return NextStep;
        }
        public void IniciarCaminadoNpc()
        {
            areaNpc.searchParameters = new SearchParameters(areaNpc.Movimientos.EndLocation, areaNpc);
            areaNpc.PathFinder = new PathFinder(areaNpc.searchParameters, areaNpc);
            List<Point> path = areaNpc.PathFinder.FindPath();
            foreach (Point point in path)
            {
                areaNpc.Movimientos.AñadirMovimiento(point.X, point.Y, 0);
            }
        }
        public void IniciarCaminado()
        {
            Session.User.searchParameters = new SearchParameters(Session.User.Movimientos.EndLocation, Session);
            Session.User.PathFinder = new PathFinder(Session.User.searchParameters, Session);
            List<Point> path = Session.User.PathFinder.FindPath();
            foreach (Point point in path)
            {
                Session.User.Movimientos.AñadirMovimiento(point.X, point.Y, 0);
            }
        }
        public bool MovementIsVerifield(Posicion NextStep)
        {
            if (Session.User.Posicion.x == NextStep.x + 1 && Session.User.Posicion.y == NextStep.y + 1) return true;
            if (Session.User.Posicion.x == NextStep.x - 1 && Session.User.Posicion.y == NextStep.y - 1) return true;
            if (Session.User.Posicion.x == NextStep.x + 1 && Session.User.Posicion.y == NextStep.y - 1) return true;
            if (Session.User.Posicion.x == NextStep.x - 1 && Session.User.Posicion.y == NextStep.y + 1) return true;
            if (Session.User.Posicion.x == NextStep.x - 1 && Session.User.Posicion.y == NextStep.y) return true;
            if (Session.User.Posicion.x == NextStep.x + 1 && Session.User.Posicion.y == NextStep.y) return true;
            if (Session.User.Posicion.x == NextStep.x && Session.User.Posicion.y == NextStep.y + 1) return true;
            if (Session.User.Posicion.x == NextStep.x && Session.User.Posicion.y == NextStep.y - 1) return true;
            return true;
        }
        public bool MovementIsVerifieldNpc(Posicion NextStep)
        {
            if (areaNpc.Posicion.x == NextStep.x + 1 && areaNpc.Posicion.y == NextStep.y + 1) return true;
            if (areaNpc.Posicion.x == NextStep.x - 1 && areaNpc.Posicion.y == NextStep.y - 1) return true;
            if (areaNpc.Posicion.x == NextStep.x + 1 && areaNpc.Posicion.y == NextStep.y - 1) return true;
            if (areaNpc.Posicion.x == NextStep.x - 1 && areaNpc.Posicion.y == NextStep.y + 1) return true;
            if (areaNpc.Posicion.x == NextStep.x - 1 && areaNpc.Posicion.y == NextStep.y) return true;
            if (areaNpc.Posicion.x == NextStep.x + 1 && areaNpc.Posicion.y == NextStep.y) return true;
            if (areaNpc.Posicion.x == NextStep.x && areaNpc.Posicion.y == NextStep.y + 1) return true;
            if (areaNpc.Posicion.x == NextStep.x && areaNpc.Posicion.y == NextStep.y - 1) return true;
            return true;
        }
    }
}
