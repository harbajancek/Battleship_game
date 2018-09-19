using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Map
    {
        public readonly MapSize mapSize;
        public readonly List<Tile> MapTiles;

        public Map(MapSize mapSize)
        {
            createMap(mapSize);
        }

        private void createMap(MapSize mapSize)
        {
            int tempY;
            int tempX;
            switch (mapSize)
            {
                case MapSize.Small:
                    tempX = 10;
                    tempY = 10;
                    break;
                case MapSize.Medium:
                    tempX = 20;
                    tempY = 20;
                    break;
                case MapSize.Big:
                    tempX = 30;
                    tempY = 30;
                    break;
                default:
                    return;
            }

            for (int i = 0; i < tempY; i++)
            {
                for (int z = 0; z < tempX; z++)
                {
                    Point tempPoint = new Point();
                    tempPoint.X = z;
                    tempPoint.Y = i;
                    Tile tempTile = Tile.NewTile(tempPoint);
                    MapTiles.Add(tempTile);
                }
            }
        }
    }
}
