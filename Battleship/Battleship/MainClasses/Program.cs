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
            
            int height = 20;
            int width = 20;
            MapSize size = new MapSize(width, height);
            TileMap Map1 = new TileMap(size);

            Ship ship = new Ship(ShipClass.Hydroplane, ShipDirection.South);
            Ship ship2 = new Ship(ShipClass.Destroyer);

            Point tempPoint = new Point();
            tempPoint.X = 8;
            tempPoint.Y = 8;
            Map1.AddShip(ship, tempPoint);

            Map1.DisplayMap();
            Console.WriteLine();

            tempPoint.X = 7;
            tempPoint.Y = 6;
            Map1.AddShip(ship2, tempPoint);

            Map1.DisplayMap();
            Console.WriteLine();

            Console.Read();
        }
    }
}
