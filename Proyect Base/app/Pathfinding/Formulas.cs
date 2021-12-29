using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Pathfinding
{
    public enum FormulasAlgoritmicas
    {
        Manhattan, MaxDXDY, DiagonalShortCut, Euclidean, EuclideanNoSQR, Other, Nula
    }
    class Formulas
    {
        public static float SolucionAlgoritmica(Point newNode, Point end, float mHEstimate)
        {
            FormulasAlgoritmicas FormulaAlgoritmica = FormulasAlgoritmicas.DiagonalShortCut;
            switch (FormulaAlgoritmica)
            {
                case FormulasAlgoritmicas.Manhattan: return Manhattan(newNode, end, mHEstimate);
                case FormulasAlgoritmicas.MaxDXDY: return MaxDXDY(newNode, end, mHEstimate);
                case FormulasAlgoritmicas.DiagonalShortCut: return DiagonalShortCut(newNode, end, mHEstimate);
                case FormulasAlgoritmicas.Euclidean: return Euclidean(newNode, end, mHEstimate);
                case FormulasAlgoritmicas.EuclideanNoSQR: return EuclideanNoSQR(newNode, end, mHEstimate);
                case FormulasAlgoritmicas.Other: return Default(newNode, end, mHEstimate);
                case FormulasAlgoritmicas.Nula: return mHEstimate;
                default: return mHEstimate;
            }
        }
        static float Manhattan(Point newNode, Point end, float mHEstimate)
        {
            return (float)mHEstimate * (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y));
        }
        static float MaxDXDY(Point newNode, Point end, float mHEstimate)
        {
            return (float)mHEstimate * (Math.Max(Math.Abs(newNode.X - end.X), Math.Abs(newNode.Y - end.Y)));
        }
        static float DiagonalShortCut(Point newNode, Point end, float mHEstimate)
        {
            int h_diagonal = Math.Min(Math.Abs(newNode.X - end.X), Math.Abs(newNode.Y - end.Y));
            int h_straight = (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y));
            return (float)(mHEstimate * 2) * h_diagonal + mHEstimate * (h_straight - 2 * h_diagonal);
        }
        static float Euclidean(Point newNode, Point end, float mHEstimate)
        {
            return (float)(mHEstimate * Math.Sqrt(Math.Pow((newNode.X - end.X), 2) + Math.Pow((newNode.Y - end.Y), 2)));
        }
        static float EuclideanNoSQR(Point newNode, Point end, float mHEstimate)
        {
            return (float)(mHEstimate * (Math.Pow((newNode.X - end.X), 2) + Math.Pow((newNode.Y - end.Y), 2)));
        }
        static float Default(Point newNode, Point end, float mHEstimate)
        {
            Point dxy = new Point(Math.Abs(end.X - newNode.X), Math.Abs(end.Y - newNode.Y));
            int Orthogonal = Math.Abs(dxy.X - dxy.Y);
            int Diagonal = Math.Abs(((dxy.X + dxy.Y) - Orthogonal) / 2);
            return (float)mHEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
        }
    }
}
