using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    internal class Algo
    {
        public static double Cross(Point p1, Point p2, Point p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }
        public static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
        public static double area(List<Point> points)
        {
            double area = 0;
            for (int i = 0; i < points.Count; i++) 
            {
                int j = (i + 1) % points.Count;
                area += points[i].X * points[j].Y - points[j].X * points[i].Y;
            }
            return Math.Abs(area) / 2.0;
        }
        public static bool isPointInside(Point point,List<Point> polygon)
        {
            double totalArea = area(polygon);
            double sumArea = 0;
            for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1) % polygon.Count;
                List<Point> trianglePoints = new List<Point> { point, polygon[i], polygon[j] };
                sumArea += area(trianglePoints);
            }
            return Math.Abs(totalArea - sumArea) < 1e-6;
        }
    }
}
