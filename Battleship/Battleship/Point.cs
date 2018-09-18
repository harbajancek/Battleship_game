using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Point
    {
        public int X { get { return X; } set { if (value >= 0) X = value; } }
        public int Y { get { return Y; } set { if (value >= 0) Y = value; } }
    }
}