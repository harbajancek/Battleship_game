using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class TileMap
    {
        public readonly List<ShipsPosition> ShipPositions = new List<ShipsPosition>();
        public readonly List<Tile> Tiles = new List<Tile>();
        public readonly MapSize Size;

        public bool AddShip(Ship ship, Point position)
        {
            Point tempPosition = new Point();
            tempPosition.X = position.X;
            tempPosition.Y = position.Y;

            if (!checkAddingNewShip(ship, position))
            {
                return false;
            }

            ShipsPosition temp = new ShipsPosition(ship, tempPosition);
            ShipPositions.Add(temp);

            return true;
        }

        private bool checkDuplicateShip(Ship ship)
        {
            foreach (var item in ShipPositions)
            {
                if (item.Ship == ship)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkAddingNewShip(Ship ship, Point position)
        {
            if (!checkCollisionNewShip(ship, position))
            {
                Console.WriteLine("Cannot add ship, collides with another.");
                return false;
            }
            else if (!checkDuplicateShip(ship))
            {
                Console.WriteLine("Cannot add ship, already added.");
                return false;
            }
            else if (!checkEdgeOfMapNewShip(ship, position))
            {
                Console.WriteLine("Cannot add ship, its parts are out of map.");
                return false;
            }

            return true;
        }

        private bool checkEdgeOfMapNewShip(Ship newShip, Point position)
        {
            foreach (var item in ShipFunctions.GetPoints(newShip, position))
            {
                if (item.X > Size.width-1 || item.Y > Size.height-1 || item.X < 0 || item.Y < 0)
                {
                    return false;
                }
            }
            
            return true;
        }

        private bool checkCollisionNewShip(Ship newShip, Point position)
        {
            List<Point> shipPoints = ShipFunctions.GetPoints(newShip, position);
            foreach (var item in shipPoints)
            {
                if (!checkAllNeihgbors(item))
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkAllNeihgbors(Point point)
        {
            foreach (MapDirection item in Enum.GetValues(typeof(MapDirection)))
            {
                if(!checkNeighborPointCollision(point, item))
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkNeighborPointCollision(Point point, MapDirection direction)
        {
            bool success = false;
            Point temp = new Point();
            switch (direction)
            {
                case MapDirection.North:
                    temp.X = point.X;
                    temp.Y = point.Y - 1;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.NorthEast:
                    temp.X = point.X + 1;
                    temp.Y = point.Y - 1;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.East:
                    temp.X = point.X + 1;
                    temp.Y = point.Y;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.SouthEast:
                    temp.X = point.X + 1;
                    temp.Y = point.Y + 1;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.South:
                    temp.X = point.X;
                    temp.Y = point.Y + 1;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.SouthWest:
                    temp.X = point.X - 1;
                    temp.Y = point.Y + 1;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.West:
                    temp.X = point.X - 1;
                    temp.Y = point.Y;
                    success = checkPointsInCollisions(temp);
                    break;
                case MapDirection.NorthWest:
                    temp.X = point.X - 1;
                    temp.Y = point.Y - 1;
                    success = checkPointsInCollisions(temp);
                    break;
                default:
                    break;
            }

            return success;
        }

        private bool checkPointsInCollisions(Point point)
        {
            foreach (var item in getAllCollisionPoints())
            {
                if (item.X == point.X && item.Y == point.Y - 1)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Point> getAllCollisionPoints()
        {
            List<Point> positions = new List<Point>();
            foreach (var item in ShipPositions)
            {
                List<Point> tempPositions = ShipFunctions.GetPoints(item.Ship, item.Position);
                positions.AddRange(tempPositions);
            }

            return positions;
        }

        public TileMap(MapSize mapSize)
        {
            Size = mapSize;
            createMap();
        }

        public void DisplayMap()
        {
            List<Point> pointList = new List<Point>();
            if(ShipPositions.Count > 0)
            {
                foreach (var shipPos in ShipPositions)
                {
                    pointList.AddRange(ShipFunctions.GetPoints(shipPos.Ship, shipPos.Position));
                }
            }

            Console.Write("  ");
            for (int i = 0; i < Size.height; i++)
            {
                Console.Write("{0} ", (char)(65+i));
            }

            Console.WriteLine();

            for (int i = 0; i < Size.height; i++)
            {
                Console.Write("{0} ", i);
                for (int z = 0; z < Size.width; z++)
                {
                    Point point = new Point();
                    point.X = z;
                    point.Y = i;
                    displayPoint(point, pointList);
                }
                Console.WriteLine();
            }
        }

        private void displayPoint(Point point, List<Point> pointList)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var item in pointList)
            {
                if(item.X == point.X && item.Y == point.Y)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
            Console.Write("■ ");
            Console.ResetColor();
        }

        private void createMap()
        {
            for (int i = 0; i < Size.height; i++)
            {
                for (int z = 0; z < Size.width; z++)
                {
                    Tile tempTile = new Tile(z, i);
                    Tiles.Add(tempTile);
                }
            }
        }

        private Point getPositionByIndex(int index)
        {
            Point position = new Point();

            if(index > (Size.height * Size.width) - 1)
            {
                throw new OverflowException();
            }

            int x;
            int y;

            if(index < Size.width)
            {
                x = Size.width;
                y = 0;
            }
            else
            {
                x = index;
                y = 0;
                while (x > Size.width-1)
                {
                    index -= Size.width;
                    y++;
                }
            }

            position.X = x;
            position.Y = y;

            return position;
        }
    }
}
