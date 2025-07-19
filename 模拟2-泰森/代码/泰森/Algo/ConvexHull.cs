using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 泰森
{
    internal class ConvexHull
    {
        public static List<Point> GetConvexHull(List<Point> points)
        {
            var pivot = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();
            points = points
                .OrderBy(o => Math.Atan2(o.Y - pivot.Y, o.X - pivot.X))
                .ThenBy(o => Algo.Distance(o, pivot))
                .ToList();

            Stack<Point> stack = new Stack<Point>();
            stack.Push(pivot);

            foreach (var point in points)
            {
                var top = stack.Pop();
                while (stack.Count > 2 && Algo.Cross(stack.Peek(), top, point) <= 0)
                    top = stack.Pop();
                stack.Push(top);
                stack.Push(point);
            }

            stack.Push(pivot);
            return stack.Reverse().ToList();
        }
    }
}
