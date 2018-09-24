using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class MapSize
    {
        public readonly int height;
        public readonly int width;
        public MapSize(int width, int height)
        {
            this.height = height;
            this.width = width;
        }
    }
}
