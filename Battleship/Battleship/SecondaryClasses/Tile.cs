using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Tile
    {
        public readonly Point Position = new Point();
        //public Ship Ship = null;
        private TileStatus status;
        public TileStatus Status
        {
            get { return status; }
        }

        public Tile(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
            status = TileStatus.NotHit;
        }


        public void Hit()
        {
            status = TileStatus.Hit;
        }
    }
}
