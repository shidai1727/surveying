using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    internal class Triangle
    {
        public List<Point> Points = new List<Point>();
        public Point Center;
        public double Radius;
        public double area;
        public bool isValid;

        public Triangle(Point p1, Point p2, Point p3)
        {
            Center = CalCenter(p1, p2, p3);

            if (Algo.Cross(p1, p2, p3) > 0)
            {
                Points.Add(p1);
                Points.Add(p2);
                Points.Add(p3);
            }
            else
            {
                Points.Add(p1);
                Points.Add(p3);
                Points.Add(p2);
            }

            Radius = Math.Sqrt((Points[0].X - Center.X) * (Points[0].X - Center.X) +
                   (Points[0].Y - Center.Y) * (Points[0].Y - Center.Y));

            Points[0].AdjacentTriangles.Add(this);
            Points[1].AdjacentTriangles.Add(this);
            Points[2].AdjacentTriangles.Add(this);

            area = Algo.area(Points);
        }

        private Point CalCenter(Point p1, Point p2, Point p3)
        {
            double dA = p1.X * p1.X + p1.Y * p1.Y;
            double dB = p2.X * p2.X + p2.Y * p2.Y;
            double dC = p3.X * p3.X + p3.Y * p3.Y;

            double A = 2 * Math.Abs(p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y));
            double Dx = dA * (p2.Y - p3.Y) + dB * (p3.Y - p1.Y) + dC * (p1.Y - p2.Y);
            double Dy = dA * (p3.X - p2.X) + dB * (p1.X - p3.X) + dC * (p2.X - p1.X);

            Point center = new Point();
            center.ID = "Center";
            center.X = Dx / A;
            center.Y = Dy / A;
            return center;
        }

        public bool IsPointInsideCircumcircle(Point point)
        {
            double distance = Algo.Distance(point, Center);
            return distance < Radius;
        }

    }
}
