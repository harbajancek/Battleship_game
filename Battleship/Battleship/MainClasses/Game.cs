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

        public void StartGame()
        {
            pickSizePhase();

            Console.Clear();

            mapOne = new TileMap(Size);
            mapTwo = new TileMap(Size);

            pickShipPhase();

            //playGamePhase();
        }

        private void playGamePhase()
        {
            /*
            while ()
            {

            }*/
        }

        private void pickShipPhase()
        {
            pickShipsToMap(mapOne);
            pickShipsToMap(mapTwo);
        }

        private void pickShipsToMap(TileMap map)
        {
            List<Ship> userShips = new List<Ship>();

            foreach (ShipClass item in Enum.GetValues(typeof(ShipClass)))
            {
                Ship tempShip = new Ship(item);
                userShips.Add(tempShip);
            }

           
            foreach (Ship ship in userShips)
            {
                Point position = new Point();
                position.X = (map.Size.width - 1) / 2;
                position.Y = (map.Size.height - 1) / 2;

                ShipsPosition shipPosition = new ShipsPosition(ship, position);

                string error = String.Empty;

                while (true)
                {
                    map.DisplayMap(shipPosition);

                    if(error != String.Empty)
                    {
                        Console.WriteLine(error);
                    }

                    var readKey = Console.ReadKey();
                    List<ConsoleKey> acceptable = new List<ConsoleKey>();
                    acceptable.Add(ConsoleKey.RightArrow);
                    acceptable.Add(ConsoleKey.LeftArrow);
                    acceptable.Add(ConsoleKey.UpArrow);
                    acceptable.Add(ConsoleKey.DownArrow);
                    acceptable.Add(ConsoleKey.R);
                    acceptable.Add(ConsoleKey.Enter);

                    if (acceptable.Contains(readKey.Key))
                    {
                        if(readKey.Key == ConsoleKey.Enter)
                        {
                            if(map.AddShip(shipPosition.Ship, shipPosition.Position))
                            {
                                error = String.Empty;
                                break;
                            }
                            else
                            {
                                error = "Can't add ship.";
                            }

                        }
                        else
                        {
                            error = String.Empty;
                            changeShipPositionByInput(readKey, shipPosition);
                        }
                    }
                    Console.Clear();
                }
                Console.Clear();
            }
        }

        private void changeShipPositionByInput(ConsoleKeyInfo key, ShipsPosition shipPosition)
        {
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    {
                        shipPosition.Position.X += 1;
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        shipPosition.Position.X -= 1;
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        shipPosition.Position.Y -= 1;
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        shipPosition.Position.Y += 1;
                        break;
                    }
                case ConsoleKey.R:
                    {
                        shipPosition.Ship.Rotate();
                        break;
                    }
                default:
                    break;
            }
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

                var readKey = Console.ReadKey();
                List<ConsoleKey> acceptable = new List<ConsoleKey>();
                acceptable.Add(ConsoleKey.NumPad1);
                acceptable.Add(ConsoleKey.NumPad2);
                acceptable.Add(ConsoleKey.NumPad3);
                acceptable.Add(ConsoleKey.NumPad4);

                if (acceptable.Contains(readKey.Key))
                {
                    Size = getMapSizeFromInput(readKey);
                    return;
                }
                Console.WriteLine("You didn't choose any option");
            }
        }

        private MapSize getMapSizeFromInput(ConsoleKeyInfo readKey)
        {
            MapSize size;
            switch (readKey.Key)
            {
                case ConsoleKey.NumPad1:
                    {
                        size = new MapSize(10, 10);
                        break;
                    }
                case ConsoleKey.NumPad2:
                    {
                        size = new MapSize(15, 15);
                        break;
                    }
                case ConsoleKey.NumPad3:
                    {
                        size = new MapSize(20, 20);
                        break;
                    }
                case ConsoleKey.NumPad4:
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
