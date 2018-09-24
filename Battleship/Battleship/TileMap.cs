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

            foreach (var point in points)
            {
                for (int i = 0; i < Tiles.Count; i++)
                {
                    if(Tiles[i].Position.X == point.X && Tiles[i].Position.Y == point.Y)
                    {
                        Tiles[i].Ship = ship;
                    }
                }
            }
            
            return true;
        }

        public TileMap(MapSize mapSize)
        {
            Size = mapSize;
            createMap();
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
