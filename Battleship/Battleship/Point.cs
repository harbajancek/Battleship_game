using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Point
    {
        private int x;
        public int X { get => x; set { if (value >= 0) { x = value; } } }
        private int y;
        public int Y { get => y; set { if (value >= 0) { y = value; } } }
    }
}