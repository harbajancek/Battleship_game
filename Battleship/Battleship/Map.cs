using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Map
    {
        public readonly int width;
        public readonly int height;
        public readonly byte[,] MapTiles;

        public Map(int inWidth, int inHeight)
        {
            width = inWidth;
            height = inHeight;
            MapTiles = new byte[width, height];
        }

        public bool AddShip(Ship ship)
        {

            /*
            int posOne;
            int posTwo;

            posOne = ship.IsHorizontal ? posX : posY;
            posTwo = ship.IsHorizontal ? posY : posX;

            for (int i = posOne; i < posOne + (int)ship.shipClass; i++)
            {
                if (!checkPointForNeighbours(i, posTwo))
                {
                    return false;
                }
            }

            if (ship.IsHorizontal)
            {
                for (int i = posY; i < posX + (int)ship.shipClass; i++)
                {
                    MapPoints[posX, i] = 1;
                }
            }
            else
            {
                for (int i = posX; i < posY + (int)ship.shipClass; i++)
                {
                    MapPoints[i, posY] = 1;
                }
            }

            return true;
            */
        }

        private bool checkPointForNeighbours(int x, int y)
        {
            byte temp;
            if (!returnPointType(out temp, x, y) || temp == 1)
            {
                return false;
            }

            int[][] values = new int[9][]
            {
                new int[] {x-1, y-1},
                new int[] {x, y-1},
                new int[] {x+1, y-1},

                new int[] {x-1, y},
                new int[] {x, y},
                new int[] {x+1, y},

                new int[] {x-1, y+1},
                new int[] {x, y+1},
                new int[] {x+1, y+1},
            };

            foreach (var item in values)
            {
                if (!returnPointType(out temp, item[0], item[1]) && temp == 1)
                {
                    return false;
                }
            }

            return true;
        }
        private bool returnPointType(out byte pointType, int x, int y)
        {
            try
            {
                pointType = MapTiles[x, y];
            }
            catch
            {
                pointType = 0;
                return false;
            }
            return true;
        }

        public bool GetPointPos(out int posX, out int posY, int arrayPosition)
        {
            posY = 0;
            posX = 0;

            if (width * height + width > arrayPosition)
            {
                return false;
            }

            for (; posY < height; posY++)
            {
                for (; posX < width; posX++)
                {
                    if (posY * posX + posX == arrayPosition)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
