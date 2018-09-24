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
            Ship ship2 = new Ship(ShipClass.Carrier);

            Map1.AddShip(ship, 55);
            Map1.AddShip(ship2, 54);

            Map1.DisplayMap();
            Console.WriteLine();

            ship.Rotate();

            Map1.DisplayMap();

            Console.Read();
        }
    }
}
