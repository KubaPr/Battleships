using System;
using System.Collections.Generic;

namespace Battleships.Core
{
    internal class FleetConfigurationProvider
    {
        public virtual List<Ship> Get()
        {
            return new List<Ship>
            {
                new Ship(5),
                new Ship(4),
                new Ship(4)
            };
        }
    }
}