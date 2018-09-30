using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Player
    {
        public string Name;
        public TileMap Map;
        public int HitCount { get; private set; }
        public int NotHitCount { get; private set; }

        public Player()
        {
            HitCount = 0;
            NotHitCount = 0;
        }

        public void Hit(bool hit)
        {
            if (hit) { HitCount++; } else { NotHitCount++; }
        }
    }
}
