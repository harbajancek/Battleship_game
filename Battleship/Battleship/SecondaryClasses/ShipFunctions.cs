using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class ShipFunctions
    {
        public static List<Point> GetPoints(Ship ship, Point point)
        {
            List<Point> pointList = new List<Point>();
            switch (ship.Class)
            {
                case ShipClass.Carrier:
                    setPointsByLength(pointList, ship, point, 5);
                    break;
                case ShipClass.Battleship:
                    setPointsByLength(pointList, ship, point, 4);
                    break;
                case ShipClass.Cruiser:
                    setPointsByLength(pointList, ship, point, 3);
                    break;
                case ShipClass.Submarine:
                    setPointsByLength(pointList, ship, point, 2);
                    break;
                case ShipClass.Destroyer:
                    pointList = new List<Point>();
                    pointList.Add(point);
                    break;
                case ShipClass.Hydroplane:
                    getPointsForHydroplane(pointList, ship, point);
                    break;
                case ShipClass.HeavyCruiser:
                    getPointsForHC(pointList, ship, point);
                    break;
                case ShipClass.Steamboat:
                    getPointsForSteamboat(pointList, ship, point);
                    break;
                default:
                    break;
            }
            return pointList;
        }
        private static void getPointsForSteamboat(List<Point> pointList, Ship ship, Point point)
        {
            pointList.Add(point);
            if (ship.Direction == ShipDirection.West)
            {
                addPoints(pointList, ShipDirection.North, point, 5);
            }
            else
            {
                addPoints(pointList, ship.Direction+1, point, 5);
            }
            
            Point tempPoint;
            tempPoint = getNeighborPointByDirection(point, ship.Direction);

            switch (ship.Direction)
            {
                case ShipDirection.North:
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.West));
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.East));
                    break;
                case ShipDirection.East:
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.North));
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.South));
                    break;
                case ShipDirection.South:
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.West));
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.East));
                    break;
                case ShipDirection.West:
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.North));
                    pointList.Add(getNeighborPointByDirection(tempPoint, ShipDirection.South));
                    break;
                default:
                    break;
            }
        }
        private static void getPointsForHC(List<Point> pointList, Ship ship, Point point)
        {
            pointList.Add(point);
            foreach (ShipDirection direction in (ShipDirection[])Enum.GetValues(typeof(ShipDirection)))
            {
                pointList.Add(getNeighborPointByDirection(point, direction));
            }
        }

        private static void getPointsForHydroplane(List<Point> pointList, Ship ship, Point point)
        {
            switch (ship.Direction)
            {
                case ShipDirection.North:
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.North));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.East));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.West));
                    break;
                case ShipDirection.East:
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.North));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.East));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.South));
                    break;
                case ShipDirection.South:
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.East));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.South));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.West));
                    break;
                case ShipDirection.West:
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.North));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.South));
                    pointList.Add(getNeighborPointByDirection(point, ShipDirection.West));
                    break;
                default:
                    break;
            }
        }

        private static Point getNeighborPointByDirection(Point point, ShipDirection direction)
        {
            Point neighbor = new Point();
            switch (direction)
            {
                case ShipDirection.North:
                    neighbor.X = point.X;
                    neighbor.Y = point.Y - 1;
                    break;
                case ShipDirection.East:
                    neighbor.X = point.X + 1;
                    neighbor.Y = point.Y;
                    break;
                case ShipDirection.South:
                    neighbor.X = point.X;
                    neighbor.Y = point.Y + 1;
                    break;
                case ShipDirection.West:
                    neighbor.X = point.X - 1;
                    neighbor.Y = point.Y;
                    break;
                default:
                    break;
            }

            return neighbor;
        }

        private static void setPointsByLength(List<Point> pointList, Ship ship, Point point, int length)
        {
            pointList.Add(point);
            addPoints(pointList, ship.Direction, point, length);
        }

        private static void addPoints(List<Point> pointList, ShipDirection direction, Point point, int length)
        {
            switch (direction)
            {
                case ShipDirection.North:
                    for (int i = 1; i < length; i++)
                    {
                        Point tempPoint = new Point();
                        if (i % 2 == 1)
                        {
                            tempPoint.X = point.X;
                            tempPoint.Y = point.Y - i + i / 2;
                            pointList.Add(tempPoint);
                        }
                        else
                        {
                            tempPoint.X = point.X;
                            tempPoint.Y = point.Y + i - i / 2;
                            pointList.Add(tempPoint);
                        }
                        
                    }
                    break;
                case ShipDirection.East:
                    for (int i = 1; i < length; i++)
                    {
                        Point tempPoint = new Point();
                        
                        if (i % 2 == 1)
                        {
                            tempPoint.X = point.X + i - i / 2;
                            tempPoint.Y = point.Y;
                            pointList.Add(tempPoint);
                        }
                        else
                        {
                            tempPoint.X = point.X - i + i / 2;
                            tempPoint.Y = point.Y;
                            pointList.Add(tempPoint);
                        }
                    }
                    break;
                case ShipDirection.South:
                    for (int i = 1; i < length; i++)
                    {
                        Point tempPoint = new Point();
                        if (i % 2 == 1)
                        {
                            tempPoint.X = point.X;
                            tempPoint.Y = point.Y + i - i / 2;
                            pointList.Add(tempPoint);
                        }
                        else
                        {
                            tempPoint.X = point.X;
                            tempPoint.Y = point.Y - i + i / 2;
                            pointList.Add(tempPoint);
                        }
                    }
                    break;
                case ShipDirection.West:
                    for (int i = 1; i < length; i++)
                    {
                        Point tempPoint = new Point();
                        if (i % 2 == 1)
                        {
                            tempPoint.X = point.X - i + i / 2;
                            tempPoint.Y = point.Y;
                            pointList.Add(tempPoint);
                        }
                        else
                        {
                            tempPoint.X = point.X + i - i / 2;
                            tempPoint.Y = point.Y;
                            pointList.Add(tempPoint);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
