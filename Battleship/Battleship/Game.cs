using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Game
    {
        private TileMap mapOne;
        private TileMap mapTwo;
        private MapSize Size;

        public Game()
        {
            pickSizePhase();

            mapOne = new TileMap(Size);
            mapTwo = new TileMap(Size);


        }

        private void pickShipPhase()
        {
            pickPhaseMap(mapOne);
            pickPhaseMap(mapTwo);
        }

        private void pickShipsToMap(TileMap map)
        {

        }

        private void pickSizePhase()
        {
            while (true)
            {
                Console.WriteLine("Choose the size of the maps by clicking the number on your keyboard");
                Console.WriteLine("1 - 10x10");
                Console.WriteLine("2 - 15x15");
                Console.WriteLine("3 - 20x20");
                Console.WriteLine("4 - Custom value");

                var key = Console.ReadKey();
                List<char> acceptable = new List<char>();
                acceptable.Add((char)(1));
                acceptable.Add((char)(2));
                acceptable.Add((char)(3));
                acceptable.Add((char)(4));

                if (acceptable.Contains(key.KeyChar))
                {
                    Size = getMapSizeFromChar(key.KeyChar);
                    return;
                }
                Console.WriteLine("You didn't choose any option");
            }
        }

        private MapSize getMapSizeFromChar(char chr)
        {
            MapSize size;
            switch (chr)
            {
                case (char)(1):
                    {
                        size = new MapSize(10, 10);
                        break;
                    }
                case (char)(2):
                    {
                        size = new MapSize(15, 15);
                        break;
                    }
                case (char)(3):
                    {
                        size = new MapSize(20, 20);
                        break;
                    }
                case (char)(4):
                    {
                        int width;
                        int height;
                        while (true)
                        {
                            Console.Write("Select width: ");
                            var pickWidth = Console.ReadLine();

                            if (int.TryParse(pickWidth, out width))
                            {
                                Console.Clear();
                                Console.WriteLine("You didn't write a number");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }

                        while (true)
                        {
                            Console.Write("Select height ");
                            var pickHeight = Console.ReadLine();

                            if (int.TryParse(pickHeight, out height))
                            {
                                Console.Clear();
                                Console.WriteLine("You didn't write a number");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }

                        size = new MapSize(width, height);
                        break;
                    }
                default:
                    return null;
            }
            return size;
        }
    }
}
