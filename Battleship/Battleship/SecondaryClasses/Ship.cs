using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        public readonly ShipClass Class;
        public ShipDirection Direction { get; private set; }
        public ShipStatus Status { get; private set; }

        public Ship(ShipClass shipClass, ShipDirection direction = ShipDirection.East)
        {
            Class = shipClass;
            this.Direction = direction;
            Status = ShipStatus.Sailing;
        }
        public void Rotate()
        {
            if ((int)Direction < 4)
            {
                Direction++;
            }
            else
            {
                Direction = ShipDirection.North;
            }
        }

        public void Sink()
        {
            Status = ShipStatus.Sunk;
        }
    }
}
