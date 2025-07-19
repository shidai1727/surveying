using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    internal class Delaunay
    {
        public static Triangle Border(List<Point> points)
        {
            double minX = points.Min(p => p.X);
            double maxX = points.Max(p => p.X);
            double minY = points.Min(p => p.Y);
            double maxY = points.Max(p => p.Y);
            
            Point p1 = new Point("A", -1e+5, -1e+5);
            Point p2 = new Point("B", 1e+5, -1e+5);
            Point p3 = new Point("C", 0, 1e+5);
            
            return new Triangle(p1, p2, p3);
        }
        public static List<Triangle> BowyerWaston(List<Point> points)
        {
            var border = Border(points);
            HashSet<Triangle> triangles = new HashSet<Triangle> { border };

            foreach (var point in points)
            {
                HashSet<Triangle> badTriangles = FindBadTriangles(triangles, point);
                HashSet<Edge> boundaryEdges = FindHoleBoundaries(badTriangles);

                triangles.RemoveWhere(t => badTriangles.Contains(t));

                foreach (var edge in boundaryEdges)
                {
                    Triangle newTriangle = new Triangle(edge.P1, edge.P2, point);
                    triangles.Add(newTriangle);
                }
            }

            triangles.RemoveWhere(o => o.Points.Any(v => border.Points.Contains(v)));
            foreach (var triangle in triangles)
            {
                triangle.isValid = true;
            }
            return triangles.ToList();
        }

        private static HashSet<Triangle> FindBadTriangles(ISet<Triangle> triangles, Point point)
        {
            HashSet<Triangle> badTriangles = new HashSet<Triangle>();
            foreach (var triangle in triangles)
            {
                if (triangle.IsPointInsideCircumcircle(point))
                {
                    badTriangles.Add(triangle);
                }
            }
            return badTriangles;
        }
        private static HashSet<Edge> FindHoleBoundaries(ISet<Triangle> badTriangles)
        {
            var edges = new List<Edge>();
            foreach (var triangle in badTriangles)
            {
                edges.Add(new Edge(triangle.Points[0], triangle.Points[1]));
                edges.Add(new Edge(triangle.Points[1], triangle.Points[2]));
                edges.Add(new Edge(triangle.Points[2], triangle.Points[0]));
            }
            HashSet<Edge> boundaryEdges = new HashSet<Edge>(
                edges.GroupBy(e => e)
                     .Where(g => g.Count() == 1)
                     .Select(g => g.First())
            );
            return boundaryEdges;
        }
    }
}
