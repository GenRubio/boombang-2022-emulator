using Proyect_Base.app.Pathfinding;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class AreaMap
    {
        public int MATRIX_X { get; set; }
        public int MATRIX_Y { get; set; }
        public double MATRIX_SCALE { get; set; }
        public double DIMENSION_X { get; set; }
        public double DIMENSION_Y { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public int MapSizeX { get; set; }
        public int MapSizeY { get; set; }
        public bool[,] BoolMap { get; set; }
        public string [] coordinates { get; set; }
        public AreaMap(DataRow row)
        {
            this.MATRIX_X = -814;
            this.MATRIX_Y = 335;
            this.MATRIX_SCALE = 1;
            setDimensionMap((int)row["id"]);
            this.posX = (int)row["posX"];
            this.posY = (int)row["posY"];
            this.coordinates = setCoordinates(Convert.ToString(row["mapa"]));
            this.BoolMap = new bool[this.coordinates.Length, this.coordinates[0].Length];
            this.MapSizeX = this.coordinates.Length;
            this.MapSizeY = this.coordinates[0].Length;
            setNewBoolMap();
        }
        //MODEL SETTERS
        private void setNewBoolMap()
        {
            for (int Y = 0; Y < this.coordinates.Length - 1; Y++)
            {
                for (int X = 0; X < this.coordinates[0].Length; X++)
                {
                    this.BoolMap[Y, X] = (this.coordinates[Y][X] == '0') ? true : false;
                }
            }
        }
        private void setDimensionMap(int mapId)
        {
            if (mapId == 44)
            {
                this.DIMENSION_X = 40.0;
                this.DIMENSION_Y = 20.0;
            }
            else
            {
                this.DIMENSION_X = 80 * this.MATRIX_SCALE;
                this.DIMENSION_Y = 40 * this.MATRIX_SCALE;
            }
        }
        private string [] setCoordinates(string map)
        {
            return map.Split(Convert.ToChar("\n"));
        }
        //MODEL GETTERS
        public int GetSizeX()
        {
            return MapSizeX;
        }
        public int GetSizeY()
        {
            return MapSizeY;
        }
        public Point GetCoordinates(Point screen)
        {
            return new Point(Convert.ToInt32(Math.Round((double)(screen.X - this.MATRIX_X) / this.DIMENSION_X + (double)(this.MATRIX_Y - screen.Y) / this.DIMENSION_Y)), Convert.ToInt32(Math.Round((double)(screen.Y - this.MATRIX_Y) / this.DIMENSION_Y + (double)(screen.X - this.MATRIX_X) / this.DIMENSION_X)));
        }
        public string GetCoordinatesTakes(Point P, string coors)
        {
            string value = "";
            if (coors != null)
            {
                string[] coordinates = coors.Split(',');// coordenates = { 0, 0}
                for (int x = 0; x < coordinates.Length; x++)
                {
                    if (x % 2 == 0)
                    {
                        int i = Convert.ToInt32(coordinates[x]);
                        coordinates[x] = (i + P.X).ToString() + ",";// 0 + p.x + ,
                    }
                    else
                    {
                        int i = Convert.ToInt32(coordinates[x]);
                        if (x < coordinates.Length - 1)
                        {
                            coordinates[x] = (i + P.Y).ToString() + ",";
                        }
                        else
                        {
                            coordinates[x] = (i + P.Y).ToString();
                        }
                    }
                    value += coordinates[x];
                }
            }
            return value;
        }
        public Point GetRandomPlace()
        {
            List<Point> Output = new List<Point>();

            for (int Y = 0; Y < this.BoolMap.GetLength(0) - 1; Y++)
            {
                for (int X = 0; X < this.BoolMap.GetLength(1) - 1; X++)
                {
                    if (this.BoolMap[Y, X])
                    {

                        Output.Add(new Point(X, Y));
                    }
                }
            }

            return (Output.Count > 0) ? Output[new Random().Next(0, Output.Count - 1)] : new Point(-1, -1);
        }
        public bool IsWalkable(int X, int Y)
        {
            try
            {
                return BoolMap[Y, X];
            }
            catch
            {
                return false;
            }
        }
        //FUNCTIONS
        public Posicion GeneratePosition()
        {
            int PosX = GetRandomPlace().X;
            int PosY = GetRandomPlace().Y;
            Posicion position = null;
            while (position == null)
            {
                position = new Posicion(PosX, PosY, 0);
                if (!IsWalkable(position.x, position.y))
                {
                    position = null;
                }
                Thread.Sleep(new TimeSpan(0, 0, 1));
            }
            return position;
        }
    }
}
