using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    class Point
    {
        public string ID;
        public double X;
        public double Y;
        public HashSet<Triangle> AdjacentTriangles = new HashSet<Triangle>();
        public Point(string id, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
        }

        public Point()
        {
        }
    }
}
