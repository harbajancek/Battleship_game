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
            Game newGame = new Game();
            newGame.StartGame();
            Console.WriteLine("Press enter to close this program...");
            Console.ReadLine();
        }
    }
}
