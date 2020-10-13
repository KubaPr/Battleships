using System.Collections.Generic;

namespace Battleships.Core.Fleet
{
    internal class FleetConfigurationProvider
    {
        public virtual List<Ship> Get()
        {
            return new List<Ship>
            {
                new Destroyer(),
                new Destroyer(),
                new Battleship()
            };
        }
    }
}