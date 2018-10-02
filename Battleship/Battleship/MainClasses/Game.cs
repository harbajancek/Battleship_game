using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Game
    {
        private Player[] Players;
        private MapSize Size;
        private List<ShipClass> pickedShipClasses = new List<ShipClass>();
        private int numberOfPlayers;

        public void StartGame()
        {
            pickNumberOfPlayers();

            pickPlayerName();

            pickSizePhase();

            pickShipClassses();

            pickShipPhase();

            playGamePhase();

            endGamePhase();
        }
        private void pickNumberOfPlayers()
        {
            while (true)
            {
                Console.Write("Pick number of players: ");
                string input = Console.ReadLine();
                int intInput;
                bool success = int.TryParse(input, out intInput);
                if (success)
                {
                    numberOfPlayers = intInput;
                    Console.Clear();
                    Players = new Player[numberOfPlayers];
                    break;
                }
                Console.WriteLine("You didn't pick a number!");
            }
            
        }
        //
        // Souhrn:
        //  Hráči si vybírají svá jména.
        //
        private void pickPlayerName()
        {
            string playerName;
            
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Write("#{0} player name: ", i+1);
                playerName = Console.ReadLine();

                Players[i] = new Player();
                Players[i].Name = playerName;
            }
            Console.Clear();
        }
        //
        // Souhrn:
        //  Určuje výšku a šířku hracího pole.
        //
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
                    Console.Clear();
                    foreach (var player in Players)
                    {
                        player.Map = new TileMap(Size);
                    }
                    return;
                }
                Console.WriteLine("You didn't choose any option");
            }
        }
        //
        // Souhrn:
        //  Uživatel vybírá hratelné třídy lodí.
        //
        private void pickShipClassses()
        {
            foreach (ShipClass shipClass in Enum.GetValues(typeof(ShipClass)))
            {
                Ship ship = new Ship(shipClass);
                Point tempPoint = new Point();
                List<Point> points = ShipFunctions.GetPoints(ship, tempPoint);

                int max_x = points.Max(item => item.X);
                int max_y = points.Max(item => item.Y);
                int min_x = points.Min(item => item.X);
                int min_y = points.Min(item => item.Y);

                while (min_x < 1)
                {
                    min_x++;
                    max_x++;
                    points.ForEach(item => item.X++);
                }

                while (min_y < 1)
                {
                    min_y++;
                    max_y++;
                    points.ForEach(item => item.Y++);
                }
                Console.WriteLine("Choose the ships:");
                Console.WriteLine("ENTER - yes   X - no");

                tempPoint = new Point();
                for (int i = 0; i < max_y + 2; i++)
                {
                    for (int z = 0; z < max_x + 2; z++)
                    {
                        tempPoint.X = z;
                        tempPoint.Y = i;
                        if (points.Any(item => item.X == tempPoint.X && item.Y == tempPoint.Y))
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

                while (true)
                {
                    List<ConsoleKey> acceptable = new List<ConsoleKey>();
                    acceptable.Add(ConsoleKey.Enter);
                    acceptable.Add(ConsoleKey.X);
                    var readKey = Console.ReadKey();

                    if (acceptable.Contains(readKey.Key))
                    {
                        if (readKey.Key == ConsoleKey.Enter)
                        {
                            pickedShipClasses.Add(shipClass);
                        }
                        break;
                    }
                }
                Console.Clear();
            }


        }
        //
        // Souhrn:
        //  Hráči si vybírají místa pro lodě. Pro každého hráče se spustí metoda pickShips.
        //
        private void pickShipPhase()
        {
            foreach (var player in Players)
            {
                pickShips(player);
            }
        }
        //
        // Souhrn:
        //  Hráč si vybírají místa pro lodě.
        //
        private void pickShips(Player player)
        {
            List<Ship> userShips = new List<Ship>();
            foreach (var item in pickedShipClasses)
            {
                Ship tempShip = new Ship(item);
                userShips.Add(tempShip);
            }
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
        //
        // Souhrn:
        //  Změní pozici lodě podle toho, jakou klávesu stiskl hráč.
        //
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
        //
        // Souhrn:
        //  Metoda, která řídí průběh hry.
        //
        private void playGamePhase()
        {
            while (Players.Count(player => player.Map.AreAllShipsSunk()) < Players.Length-1)
            {
                for (int i = 0; i < Players.Length; i++)
                {
                    int enemyPlayerIndex = pickEnemyPlayer(i);
                    pickTarget(i, enemyPlayerIndex);
                }
            }
        }
        private int pickEnemyPlayer(int playerIndex)
        {
            List<int> accebtableList = new List<int>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Players[playerIndex].Name);
            Console.ResetColor();
            Console.WriteLine("'s turn");
            Console.WriteLine("Pick your enemy:");
            for (int i = 0; i < Players.Length; i++)
            {
                if (i != playerIndex)
                {
                    Console.WriteLine("{0} - {1}", i+1, Players[i].Name);
                    accebtableList.Add(i+1);
                }
            }

            while (true)
            {
                var input = Console.ReadKey();
                int index;
                string inputString = input.KeyChar.ToString();
                bool success = int.TryParse(inputString, out index);

                if (success)
                {
                    if (accebtableList.Contains(index))
                    {
                        Console.Clear();
                        return index-1;
                    }
                }
                
                Console.WriteLine("You didn't pick a number");
            }
        }
        //
        // Souhrn:
        //  Hráč si vybírá políčko, na které zaútočí.
        //
        private void pickTarget(int PlayerIndexOne, int PlayerIndexTwo)
        {
            Player player = Players[PlayerIndexOne];
            TileMap enemyMap = Players[PlayerIndexTwo].Map;

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
            bool hit;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(player.Name);
                Console.ResetColor();
                Console.Write("'s turn to attack ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Players[PlayerIndexTwo].Name);
                Console.ResetColor();
                Console.WriteLine();

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
                        if (enemyMap.AddTarget(out message, out hit, target))
                        {
                            Console.WriteLine(message);
                            player.Hit(hit);
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
        //
        // Souhrn:
        //  Změní pozici políčka podle toho, jakou klávesu stiskl hráč.
        //
        private void changeTargetByInput(Point target, ConsoleKeyInfo key, TileMap map)
        {
            Point newPoint = new Point();
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    {
                        newPoint.X = target.X + 1;
                        newPoint.Y = target.Y;
                        if (!(newPoint.X > map.Size.width - 1 || newPoint.Y > map.Size.height - 1 || newPoint.X < 0 || newPoint.Y < 0))
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
        //
        // Souhrn:
        //  Vypíše výherce a statistiky.
        //
        private void endGamePhase()
        {
            Console.Write("Game result: ");
            Player winner;
            if (Players.All(player => player.Map.AreAllShipsSunk()))
            {
                winner = null;
            }
            else
            {
                winner = Players.First(player => player.Map.AreAllShipsSunk());
            }

            if (winner == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("TIE!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0} WINS!", winner.Name);
            }
            Console.ResetColor();
            Console.WriteLine();

            foreach (var player in Players)
            {
                Console.WriteLine("{0}'s game statistics:", player.Name);
                Console.WriteLine("Ship tiles hit: {0}", player.HitCount);
                Console.WriteLine("Ship tiles NOT hit: {0}", player.NotHitCount);
                Console.WriteLine("Accuracy: {0}%", (int)(((float)player.HitCount / ((float)player.HitCount + (float)player.NotHitCount)) * 100));
                player.Map.DisplayMap();
                Console.WriteLine();
            }
        }
        //
        // Souhrn:
        //  Vrátí velikost mapy podle toho, co vybral nebo vybere uživatel.
        //
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
                            else if (width == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Number must be at least 2 or more");
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
                            else if (width == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Number must be at least 2 or more");
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
