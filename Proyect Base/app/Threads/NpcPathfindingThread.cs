using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Models;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Threads
{
    class NpcPathfindingThread
    {
        public static void init()
        {
            new Thread(Pathfinding).Start();
        }
        private static void Pathfinding()
        {
            List<PublicArea> publicAreas = getAreasWithNpcWalk();
            while (true)
            {
                try
                {
                    foreach (PublicArea publicArea in publicAreas)
                    {
                        List<AreaNpc> npcs = publicArea.areaNpcs.Where(i => i.poster_model == 0 && i.poster_type == 0).ToList();
                        foreach(AreaNpc areaNpc in npcs)
                        {
                            moveNpcToNextPoint(publicArea, areaNpc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.error(ex);
                }
                Thread.Sleep(1);
            }
        }
        private static List<PublicArea> getAreasWithNpcWalk()
        {
            List<PublicArea> publicAreas = new List<PublicArea>();
            foreach(PublicArea publicArea in PublicAreaCollection.publicAreas.Values)
            {
                if (publicArea.areaNpcs.Find(i => i.poster_model == 0 && i.poster_type == 0) != null)
                {
                    publicAreas.Add(publicArea);
                }
            }
            return publicAreas;
        }
        private static void moveNpcToNextPoint(PublicArea publicArea, AreaNpc areaNpc)
        {
            if (checkOldMoviments(areaNpc))
            {
                Posicion NewPoint = areaNpc.Movimientos.SiguienteMovimientoNpc();
                if (validateNextMoviment(areaNpc, NewPoint))
                {
                    if (areaNpc.getArea().MapaBytes.IsWalkable(NewPoint.x, NewPoint.y))
                    {
                        setNextPositionNpc(areaNpc, NewPoint);
                        areaNpc.sendWalkHandler(publicArea);
                    }
                    else
                    {
                        areaNpc.Movimientos.Movimientos.Clear();
                    }
                }
            }
            else if (areaNpc.Movimientos == null || areaNpc.Movimientos.Movimientos.Count == 0)
            {
                nexWalkPositions(areaNpc);
            }
        }
        private static void setNextPositionNpc(AreaNpc areaNpc, Posicion NewPoint)
        {
            areaNpc.Bloqueos.SetLock(Bloqueo.Caminando, TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 680));
            areaNpc.Posicion = NewPoint;
            areaNpc.LastPoint = NewPoint;
            areaNpc.Movimientos.Movimientos.Remove(areaNpc.Posicion);
        }
        private static bool validateNextMoviment(AreaNpc areaNpc, Posicion NewPoint)
        {
            if (areaNpc.Movimientos.MovementIsVerifieldNpc(NewPoint)
                && !areaNpc.Bloqueos.IsBlock(Bloqueo.Block)
                && !areaNpc.Bloqueos.IsBlock(Bloqueo.Caminando))
            {
                return true;
            }
            return false;
        }
        private static bool checkOldMoviments(AreaNpc areaNpc)
        {
            if (areaNpc.Movimientos != null && areaNpc.Movimientos.Movimientos.Count != 0)
            {
                return true;
            }
            return false;
        }
        private static List<string> npcWalkCoordenates()
        {
            List<string> positions = new List<string>();
            positions.Add("11127111371114711157111671117711187111971120711217");
            positions.Add("101240913408144071540615805158");
            positions.Add("1212113125141251512516125");
            positions.Add("121211313113147");
            positions.Add("12103131051410515105");
            positions.Add("10124091340814407154071670717707187");
            positions.Add("10102090920908609076");
            return positions;
        }
        private static void nexWalkPositions(AreaNpc areaNpc)
        {
            areaNpc.Bloqueos = new PreLocks();
            areaNpc.Ultra_Bloqueos = new UltraLocks();
            areaNpc.Movimientos = new Trayectoria(areaNpc);
            List<Posicion> ListPositions = new List<Posicion>();
            List<string> positions = npcWalkCoordenates();
            string path = positions[new Random().Next(0, positions.Count())];
            while (path != "")
            {
                int x = int.Parse(path.Substring(0, 2));
                int y = int.Parse(path.Substring(2, 2));
                int z = int.Parse(path.Substring(4, 1));
                ListPositions.Add(new Posicion(x, y, z));
                path = path.Substring(5);
            }

            ListPositions.Reverse();
            areaNpc.Movimientos.EndLocation = new Point(ListPositions[0].x, ListPositions[0].y);
            areaNpc.Movimientos.IniciarCaminadoNpc();
        }
    }
}
