using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    class File
    {
        public static List<Point> LoadFile(string filePath)
        {
            List<Point> points = new List<Point>();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++) 
            {
                string line = lines[i];
                string[] parts = line.Split(' ');
                Point point = new Point
                {
                    ID = i.ToString(),
                    X = double.Parse(parts[0]),
                    Y = double.Parse(parts[1])
                };
                points.Add(point);
            }
            return points;
        }
    }
}
