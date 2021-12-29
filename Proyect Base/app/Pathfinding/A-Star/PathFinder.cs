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
    public class PathFinder
    {
        private int width;
        private int height;
        private Node[,] nodes;
        private Node startNode;
        private Node endNode;
        private SearchParameters searchParameters;
        private Area Sala;
        private Session Session;
        public PathFinder(SearchParameters searchParameters, Session Session)
        {
            this.searchParameters = searchParameters;
            InitializeNodes(searchParameters.Map);
            this.startNode = this.nodes[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
            this.startNode.State = NodeState.Open;
            this.endNode = this.nodes[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];
            this.Sala = searchParameters.Sala;
            this.Session = Session;
        }
        public List<Point> FindPath()
        {
            List<Point> path = new List<Point>();
            bool success = Search(startNode);
            if (success)
            {
                Node node = this.endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                path.Reverse();
            }
            return path;
        }

        private void InitializeNodes(bool[,] map)
        {
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);
            this.nodes = new Node[this.width, this.height];
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    this.nodes[x, y] = new Node(x, y, map[x, y], this.searchParameters.EndLocation);
                }
            }
        }

        private bool Search(Node currentNode)
        {
            currentNode.State = NodeState.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == this.endNode.Location)
                {
                    return true;
                }
                else
                {
                    if (Search(nextNode))
                        return true;
                }
            }
            return false;
        }

        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);
            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                    continue;

                Node node = this.nodes[x, y];

                if (!Sala.MapaBytes.IsWalkable(node.Location.X, node.Location.Y))
                    continue;

                if (Sala.getSession(node.Location.X, node.Location.Y) != null)
                    continue;

                if (node.State == NodeState.Closed)
                    continue;

                if (node.State == NodeState.Open)
                {
                    float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return new Point[]
            {
                new Point(fromLocation.X+1, fromLocation.Y+1),
                new Point(fromLocation.X-1, fromLocation.Y-1),
                new Point(fromLocation.X+1, fromLocation.Y),
                new Point(fromLocation.X, fromLocation.Y+1),
                new Point(fromLocation.X-1, fromLocation.Y),
                new Point(fromLocation.X, fromLocation.Y-1),
                new Point(fromLocation.X+1, fromLocation.Y-1),
                new Point(fromLocation.X-1, fromLocation.Y+1)
            };
        }
    }
}
