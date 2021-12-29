using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Pathfinding.A_Star
{
    public class Node
    {
        private Node parentNode;
        public Point Location { get; private set; }
        public bool IsWalkable { get; set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public NodeState State { get; set; }
        public float F
        {
            get { return this.G + this.H; }
        }
        public Node ParentNode
        {
            get { return this.parentNode; }
            set
            {
                this.parentNode = value;
                this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location);
            }
        }

        public Node(int x, int y, bool isWalkable, Point endLocation)
        {
            this.Location = new Point(x, y);
            this.State = NodeState.Untested;
            this.IsWalkable = isWalkable;
            this.H = GetTraversalCost(this.Location, endLocation);
            this.G = 0;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}: {2}", this.Location.X, this.Location.Y, this.State);
        }

        internal static float GetTraversalCost(Point newNode, Point end)
        {
            float mHEstimate;
            float deltaX = end.X - newNode.X;
            float deltaY = end.Y - newNode.Y;
            mHEstimate = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return Formulas.SolucionAlgoritmica(newNode, end, mHEstimate);
        }
    }
}
