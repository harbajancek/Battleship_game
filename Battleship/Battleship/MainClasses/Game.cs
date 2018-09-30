using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Game
    {
        private List<Player> Players = new List<Player>();
        private MapSize Size;

        public void StartGame()
        {
            pickSizePhase();

            Console.Clear();

            Player player = new Player
            {
                Map = new TileMap(Size),
                Name = "Anon 1"
            };

            Players.Add(player);

            player = new Player
            {
                Map = new TileMap(Size),
                Name = "Anon 2"
            };

            Players.Add(player);
            pickShipPhase();

            playGamePhase();
        }

        private void playGamePhase()
        {

            while (!Players.Any(player => player.Map.AreAllShipsSunk()))
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    pickTarget(i);
                }
            }
        }

        private void pickTarget(int PlayerIndex)
        {
            string playerName = Players[PlayerIndex].Name;
            TileMap enemyMap;

            if(PlayerIndex == 1)
            {
                enemyMap = Players[0].Map;
            }
            else
            {
                enemyMap = Players[1].Map;
            }

            List<ConsoleKey> acceptable = new List<ConsoleKey>();
            acceptable.Add(ConsoleKey.RightArrow);
            acceptable.Add(ConsoleKey.LeftArrow);
            acceptable.Add(ConsoleKey.UpArrow);
            acceptable.Add(ConsoleKey.DownArrow);
            acceptable.Add(ConsoleKey.Enter);

            Point target = new Point();
            target.X = (enemyMap.Size.width - 1) / 2;
            target.Y = (enemyMap.Size.height - 1) / 2;

            string message = String.Empty;
            while (true)
            {
                Console.WriteLine("{0}'s turn to pick a target tile.", Players[PlayerIndex].Name);
                enemyMap.DisplayMap(target);

                if (message != String.Empty)
                {
                    Console.WriteLine(message);
                }

                var readKey = Console.ReadKey();


                if (acceptable.Contains(readKey.Key))
                {
                    if (readKey.Key == ConsoleKey.Enter)
                    {
                        if (enemyMap.AddTarget(out message, target))
                        {
                            Console.WriteLine(message);
                            System.Threading.Thread.Sleep(1200);
                            break;
                        }

                    }
                    else
                    {
                        message = String.Empty;
                        changeTargetByInput(target, readKey, enemyMap);
                    }
                }
                Console.Clear();
            }
            Console.Clear();
        }

        private void pickShipPhase()
        {
            Players.ForEach(player => pickShips(player));
        }

        private void pickShips(Player player)
        {
            List<Ship> userShips = new List<Ship>();

            Ship tempShip = new Ship(ShipClass.Submarine);
            userShips.Add(tempShip);

            /*
            foreach (ShipClass item in Enum.GetValues(typeof(ShipClass)))
            {
                Ship tempShip = new Ship(item);
                userShips.Add(tempShip);
            }*/

           
            foreach (Ship ship in userShips)
            {
                Point position = new Point();
                position.X = (player.Map.Size.width - 1) / 2;
                position.Y = (player.Map.Size.height - 1) / 2;

                ShipsPosition shipPosition = new ShipsPosition(ship, position);

                string error = String.Empty;

                List<ConsoleKey> acceptable = new List<ConsoleKey>();
                acceptable.Add(ConsoleKey.RightArrow);
                acceptable.Add(ConsoleKey.LeftArrow);
                acceptable.Add(ConsoleKey.UpArrow);
                acceptable.Add(ConsoleKey.DownArrow);
                acceptable.Add(ConsoleKey.R);
                acceptable.Add(ConsoleKey.Enter);

                while (true)
                {
                    Console.WriteLine("{0}'s turn", player.Name);
                    player.Map.DisplayMap(shipPosition);

                    if(error != String.Empty)
                    {
                        Console.WriteLine(error);
                    }

                    var readKey = Console.ReadKey();
                    

                    if (acceptable.Contains(readKey.Key))
                    {
                        if(readKey.Key == ConsoleKey.Enter)
                        {
                            if(player.Map.AddShip(shipPosition.Ship, shipPosition.Position))
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
                            changeShipPositionByInput(readKey, shipPosition, player.Map);
                        }
                    }
                    Console.Clear();
                }
                Console.Clear();
            }
        }
        private void changeTargetByInput(Point target, ConsoleKeyInfo key, TileMap map)
        {
            Point newPoint = new Point();
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    {
                        newPoint.X = target.X + 1;
                        newPoint.Y = target.Y;
                        if(!(newPoint.X > map.Size.width - 1 || newPoint.Y > map.Size.height - 1 || newPoint.X < 0 || newPoint.Y < 0))
                        {
                            target.X = newPoint.X;
                            target.Y = newPoint.Y;
                        }
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        newPoint.X = target.X - 1;
                        newPoint.Y = target.Y;
                        if (!(newPoint.X > map.Size.width - 1 || newPoint.Y > map.Size.height - 1 || newPoint.X < 0 || newPoint.Y < 0))
                        {
                            target.X = newPoint.X;
                            target.Y = newPoint.Y;
                        }
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        newPoint.X = target.X;
                        newPoint.Y = target.Y - 1;
                        if (!(newPoint.X > map.Size.width - 1 || newPoint.Y > map.Size.height - 1 || newPoint.X < 0 || newPoint.Y < 0))
                        {
                            target.X = newPoint.X;
                            target.Y = newPoint.Y;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        newPoint.X = target.X;
                        newPoint.Y = target.Y + 1;
                        if (!(newPoint.X > map.Size.width - 1 || newPoint.Y > map.Size.height - 1 || newPoint.X < 0 || newPoint.Y < 0))
                        {
                            target.X = newPoint.X;
                            target.Y = newPoint.Y;
                        }
                        break;
                    }
                default:
                    break;
            }
        }


        private void changeShipPositionByInput(ConsoleKeyInfo key, ShipsPosition shipPosition, TileMap map)
        {
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    {
                        Point newPoint = new Point();
                        newPoint.X = shipPosition.Position.X + 1;
                        newPoint.Y = shipPosition.Position.Y;
                        if(map.CheckEdgeOfMapShip(shipPosition.Ship, newPoint))
                        {
                            shipPosition.Position = newPoint;
                        }
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        Point newPoint = new Point();
                        newPoint.X = shipPosition.Position.X - 1;
                        newPoint.Y = shipPosition.Position.Y;
                        if (map.CheckEdgeOfMapShip(shipPosition.Ship, newPoint))
                        {
                            shipPosition.Position = newPoint;
                        }
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        Point newPoint = new Point();
                        newPoint.X = shipPosition.Position.X;
                        newPoint.Y = shipPosition.Position.Y - 1;
                        if (map.CheckEdgeOfMapShip(shipPosition.Ship, newPoint))
                        {
                            shipPosition.Position = newPoint;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        Point newPoint = new Point();
                        newPoint.X = shipPosition.Position.X;
                        newPoint.Y = shipPosition.Position.Y + 1;
                        if (map.CheckEdgeOfMapShip(shipPosition.Ship, newPoint))
                        {
                            shipPosition.Position = newPoint;
                        }
                        break;
                    }
                case ConsoleKey.R:
                    {
                        Ship tempShip = new Ship(shipPosition.Ship.Class, shipPosition.Ship.Direction);
                        for (int i = 0; i < 3; i++)
                        {
                            tempShip.Rotate();
                            if (map.CheckEdgeOfMapShip(tempShip, shipPosition.Position))
                            {
                                shipPosition.Ship = tempShip;
                                break;
                            }
                        }
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
                        Console.Clear();
                        while (true)
                        {
                            Console.Write("Select width: ");
                            var pickWidth = Console.ReadLine();

                            if (!int.TryParse(pickWidth, out width))
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

                        Console.Clear();
                        while (true)
                        {
                            Console.Write("Select height: ");
                            var pickHeight = Console.ReadLine();

                            if (!int.TryParse(pickHeight, out height))
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
