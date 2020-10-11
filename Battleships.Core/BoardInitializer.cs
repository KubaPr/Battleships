using System.Collections.Generic;

namespace Battleships.Core
{
    internal class BoardInitializer
    {
        private readonly FleetPositioner _fleetPositioner;
        private readonly FleetConfigurationProvider _fleetConfigurationProvider;

        public BoardInitializer(FleetPositioner fleetPositioner, FleetConfigurationProvider fleetConfigurationProvider)
        {
            _fleetPositioner = fleetPositioner;
            _fleetConfigurationProvider = fleetConfigurationProvider;
        }

        public Board Initialize()
        {
            var fleet = _fleetConfigurationProvider.Get();

            return new Board(_fleetPositioner.CreatePositions(fleet));
        }
    }
}
