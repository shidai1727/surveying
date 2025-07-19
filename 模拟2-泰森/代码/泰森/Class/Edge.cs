using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    class Edge
    {
        public Point P1;
        public Point P2;
        public Edge(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public override string ToString()
        {
            return $"Edge: ({P1.X}, {P1.Y}) - ({P2.X}, {P2.Y})";
        }
        public override bool Equals(object obj)
        {
            var edge = obj as Edge;

            var samePoints = P1 == edge.P1 && P2 == edge.P2;
            var samePointsReversed = P1 == edge.P2 && P2 == edge.P1;
            return samePoints || samePointsReversed;
        }
        public override int GetHashCode()
        {
            int hCode = (int)P1.X ^ (int)P1.Y ^ (int)P2.X ^ (int)P2.Y;
            return hCode.GetHashCode();
        }
    }
}
