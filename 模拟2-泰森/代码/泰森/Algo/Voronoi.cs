using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    internal class Voronoi
    {
        public static List<Polygon> GetVoronoi(List<Point> points,List<Point> convexHull)
        {
            //points = points.Where(o => !convexHull.Contains(o)).ToList();
            var Polygons = new List<Polygon>();

            foreach(var point in points)
            {
                var polygon = new Polygon();
                var Triangles = point.AdjacentTriangles.Where(o => o.isValid);
                var TriangleCenters = Triangles.Select(t => t.Center).ToList();
                if (TriangleCenters.Any(o => !Algo.isPointInside(o, convexHull))) continue;
                polygon.Points = ConvexHull.GetConvexHull(TriangleCenters);
                polygon.area = Algo.area(polygon.Points);
                Polygons.Add(polygon);
            }
            return Polygons;
        }
    }
}
