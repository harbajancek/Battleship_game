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
        public readonly ShipState shipState = ShipState.NotSunk;
        public Point Point;

        public Ship(ShipClass shipClass, Point point, Direction direction = Direction.North)
        {
            this.shipClass = shipClass;
            this.direction = direction;
            this.Point = point;
        }

        public List<Point> GetShipTiles()
        {
            List<Point> tiles = new List<Point>();
            switch (shipClass)
            {
                case ShipClass.Carrier:
                    return tiles;
                case ShipClass.Battleship:
                    return tiles;
                case ShipClass.Cruiser:
                    return tiles;
                case ShipClass.Submarine:
                    return tiles;
                case ShipClass.Destroyer:
                    return tiles;
                default:
                    return tiles;
            }
        }
    }
}
