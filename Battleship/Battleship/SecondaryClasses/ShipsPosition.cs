using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class ShipsPosition
    {
        public Ship Ship;
        public Point Position = new Point();

        public ShipsPosition(Ship ship, Point position)
        {
            Ship = ship;
            Position = position;
        }
    }
}
