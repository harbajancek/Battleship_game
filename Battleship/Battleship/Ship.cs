using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        public readonly ShipClass shipClass;
        public readonly Direction direction;
        public readonly ShipState shipState;
        public Point Position;

        public Ship(ShipClass shipClass, Point point, Direction direction = Direction.North)
        {
            this.shipClass = shipClass;
            this.direction = direction;
            this.Position = point;
        }

        public List<Point> GetShipPoints()
        {
            List<Point> points = new List<Point>();

            switch (shipClass)
            {
                case ShipClass.Carrier:
                    points = getShipPointsByLength(5);
                    break;
                case ShipClass.Battleship:
                    points = getShipPointsByLength(4);
                    break;
                case ShipClass.Cruiser:
                    points = getShipPointsByLength(3);
                    break;
                case ShipClass.Submarine:
                    points = getShipPointsByLength(2);
                    break;
                case ShipClass.Destroyer:
                    points = getShipPointsByLength(1);
                    break;
                case ShipClass.Hydroplane:
                    break;
                case ShipClass.HeavyCruiser:
                    break;
                default:
                    break;
            }

            return points;
        }

        private List<Point> getShipPointsByLength(int length)
        {
            List<Point> points = new List<Point>();
            points.Add(Position);

            for (int i = 1; i < length; i++)
            {
                Point tempPos = new Point();
                switch (direction)
                {
                    case Direction.North:
                        tempPos.X = Position.X;
                        tempPos.Y = Position.Y - i;
                        break;
                    case Direction.East:
                        tempPos.X = Position.X - i;
                        tempPos.Y = Position.Y;
                        break;
                    case Direction.South:
                        tempPos.X = Position.X;
                        tempPos.Y = Position.Y + i;
                        break;
                    case Direction.West:
                        tempPos.X = Position.X + i;
                        tempPos.Y = Position.Y;
                        break;
                    default:
                        break;
                }
                points.Add(tempPos);
            }
            return points;
        }
    }
}
