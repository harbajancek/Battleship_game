using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Tile
    {
        public Point Point;
        public Ship Ship;
        public TileState TileState;

        public static Tile NewTile(Point point, TileState tileState = TileState.NotHit, Ship ship = null)
        {
            Tile tile = new Tile();

            tile.Point = point;
            tile.Ship = ship;
            tile.TileState = tileState;

            return tile;
        }
    }
}
