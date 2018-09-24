using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class TileMap
    {
        public readonly List<Tile> Tiles = new List<Tile>();
        public readonly MapSize Size;

        public bool AddShip(Ship ship, int index)
        {
            Point position = Tiles[index].Position;
            List<Point> points = ShipFunctions.GetPoints(ship, position);

            foreach (var point in points)
            {
                if(point.X < 0 || point.Y < 0)
                {
                    return false;
                }
            }

            Tiles[index].Ship = ship;
            return true;
        }

        public TileMap(MapSize mapSize)
        {
            Size = mapSize;
            createMap();
        }

        public void DisplayMap()
        {
            var query = from tile in Tiles
                        where tile.Ship != null
                        select tile;

            List<Point> pointList = new List<Point>();
            foreach (var tile in query)
            {
                pointList.AddRange(ShipFunctions.GetPoints(tile.Ship, tile.Position));
            }

            List<Point> overflowedPositions = new List<Point>();

            for (int i = 0; i < pointList.Count - 1; i++)
            {
                for (int z = 1 + i; z < pointList.Count; z++)
                {
                    if (pointList[i].X == pointList[z].X && pointList[i].Y == pointList[z].Y)
                    {
                        overflowedPositions.Add(pointList[i]);
                    }
                }
            }

            int index;
            for (int i = 0; i < Size.height; i++)
            {
                for (int z = 0; z < Size.width; z++)
                {
                    index = Size.width * i + z;
                    Point position = Tiles[index].Position;
                    int pointType = 0;

                    foreach (var point in overflowedPositions)
                    {
                        if(position.X == point.X && position.Y == point.Y)
                        {
                            pointType = 2;
                            break;
                        }
                    }

                    if (pointType == 0)
                    {
                        foreach (var point in pointList)
                        {
                            if (position.X == point.X && position.Y == point.Y)
                            {
                                pointType = 1;
                                break;
                            }
                        }
                    }

                    if (pointType == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if(pointType == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write("■ ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
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
    }
}
