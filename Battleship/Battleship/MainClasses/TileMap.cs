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
        public readonly List<Point> AttackedTiles = new List<Point>();
        public readonly MapSize Size;

        public bool AddTarget(out string message, out bool hit, Point target)
        {
            if (AttackedTiles.Any(item => item.X == target.X && item.Y == target.Y))
            {
                message = "Tile has been already attacked.";
                hit = false;
                return false;
            }

            if (getPointsFromPlacedShips().Any(item => item.X == target.X && item.Y == target.Y))
            {
                AttackedTiles.Add(target);
                message = "You hit an enemy ship!";
                hit = true;
                return true;
            }
            else
            {
                message = "You hit nothing";
                hit = false;
                return true;
            }
        }

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
            if (!checkCollisionShip(ship, position))
            {
                Console.WriteLine("Cannot add ship, collides with another.");
                return false;
            }
            else if (!checkDuplicateShip(ship))
            {
                Console.WriteLine("Cannot add ship, already added.");
                return false;
            }
            else if (!CheckEdgeOfMapShip(ship, position))
            {
                Console.WriteLine("Cannot add ship, its parts are out of map.");
                return false;
            }

            return true;
        }

        public bool CheckEdgeOfMapShip(Ship newShip, Point position)
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

        private bool checkCollisionShip(Ship ship, Point position)
        {
            List<Point> shipPoints = ShipFunctions.GetPoints(ship, position);
            foreach (var item in shipPoints)
            {
                if (!checkAllNeihgbors(item))
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkCollisionPointList(List<Point> points)
        {
            foreach (var item in points)
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
                if (item.X == point.X && item.Y == point.Y)
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
        }

        // zobrazení při pokládání lodě
        public void DisplayMap(ShipsPosition newShip)
        {
            List<Point> newShipPoints = new List<Point>();
            newShipPoints = ShipFunctions.GetPoints(newShip.Ship, newShip.Position);

            List<Point> pointList = getPointsFromPlacedShips();

            Console.Write("   ");
            for (int i = 0; i < Size.width; i++)
            {
                Console.Write("{0} ", (char)(65 + i));
            }

            Console.WriteLine();

            for (int i = 0; i < Size.height; i++)
            {
                Console.Write("   ");
                for (int z = 0; z < Size.width; z++)
                {
                    Point point = new Point();
                    point.X = z;
                    point.Y = i;
                    displayPoint(point, pointList, newShipPoints);
                        
                }

                Console.Write("{0} ", i + 1);
                Console.WriteLine();
            }
        }

        // normální zobrazení
        public void DisplayMap(Point enemySelect = null)
        {
            List<Point> pointList = getPointsFromPlacedShips();

            Console.Write("   ");
            for (int i = 0; i < Size.height; i++)
            {
                Console.Write("{0} ", (char)(65 + i));
            }

            Console.WriteLine();

            for (int i = 0; i < Size.height; i++)
            {
                Console.Write("   ");
                for (int z = 0; z < Size.width; z++)
                {
                    Point point = new Point();
                    point.X = z;
                    point.Y = i;
                    displayPoint(point, pointList, enemySelect);

                }

                Console.Write("{0} ", i + 1);
                Console.WriteLine();
            }
        }

        // normální zobrazení
        private void displayPoint(Point point, List<Point> pointList, Point enemySelect = null)
        {
            string dPoint = "■";
            Console.ForegroundColor = ConsoleColor.DarkGray;
            bool isShip = false;

            foreach (var shipPoint in pointList)
            {
                if (shipPoint.X == point.X && shipPoint.Y == point.Y)
                {
                    if (enemySelect == null)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    isShip = true;
                    break;
                }
            }
            foreach (var tile in AttackedTiles)
            {
                if (tile.X == point.X && tile.Y == point.Y)
                {
                    if (isShip)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }
                    break;

                }
            }

            if (enemySelect != null)
            {
                if (point.X == enemySelect.X && point.Y == enemySelect.Y)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
            }


            Console.Write(dPoint);
            Console.ResetColor();
            Console.Write(" ");
        }

        // zobrazení při pokládání lodě
        private void displayPoint(Point point, List<Point> pointList, List<Point> newShipPoints)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var item in pointList)
            {
                if(item.X == point.X && item.Y == point.Y)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            foreach (var item in newShipPoints)
            {
                if (item.X == point.X && item.Y == point.Y)
                {
                    if (checkAllNeihgbors(item))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                        
                }
            }

            Console.Write("■");
            Console.ResetColor();
            Console.Write(" ");
        }

        private List<Point> getPointsFromPlacedShips()
        {
            List<Point> pointList = new List<Point>();
            if (ShipPositions.Count > 0)
            {
                foreach (var shipPos in ShipPositions)
                {
                    pointList.AddRange(ShipFunctions.GetPoints(shipPos.Ship, shipPos.Position));
                }
            }
            return pointList;
        }

        public bool AreAllShipsSunk()
        {
            return ShipPositions.All(item => isShipSunk(item));
        }

        public bool isShipSunk(ShipsPosition shipPosition)
        {
            if(shipPosition.Ship.Status == ShipStatus.Sunk)
            {
                return true;
            }
            else
            {
                List<Point> shipPoints = ShipFunctions.GetPoints(shipPosition.Ship, shipPosition.Position);

                bool result = shipPoints.All(item => AttackedTiles.Any(tile => item.X == tile.X && item.Y == tile.Y));

                if (result)
                {
                    shipPosition.Ship.Sink();
                    return true;
                }
                return false;
            }
        }
    }
}
