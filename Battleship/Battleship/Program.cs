using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 10;
            int width = 10;
            MapSize size = new MapSize(width, height);
            TileMap Map1 = new TileMap(size);

            Ship ship = new Ship(ShipClass.Hydroplane);

            Map1.AddShip(ship, 55);

            int index;
            for (int i = 0; i < Map1.Size.height; i++)
            {
                for (int z = 0; z < Map1.Size.width; z++)
                {
                    index = Map1.Size.width * i + z;
                    if(Map1.Tiles[index].Ship != null)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.Write("■ ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            ship.Rotate();

            for (int i = 0; i < Map1.Size.height; i++)
            {
                for (int z = 0; z < Map1.Size.width; z++)
                {
                    index = Map1.Size.width * i + z;
                    if (Map1.Tiles[index].Ship != null)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.Write("■ ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            /*
            index = 0;
            foreach (var item in Map1.Tiles)
            {
                if (item.Ship == null)
                {
                    Console.WriteLine("{0}: 0", index);
                }
                else
                {
                    Console.WriteLine("{0}: 1 ", index);
                }
                index++;
            }
            Console.WriteLine();
            */
            Console.Read();
        }
    }
}
