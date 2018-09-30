using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class PointFactory
    {
        private static List<Point> points;
        public static Point GetPoint(int x, int y)
        {
            var query = from point in points
                         where point.X == x && point.Y == y
                         select point;


            if(query.Count<Point>() == 0)
            {
                Point tempPoint = newPoint(x,y);
                points.Add(tempPoint);
                return points.Last<Point>();
            }
            else
            {
                return query.First<Point>();
            }
        }

        private static Point newPoint(int x, int y)
        {
            Point tempPoint = new Point();
            tempPoint.X = x;
            tempPoint.Y = y;
            return tempPoint;
        }
    }
}
