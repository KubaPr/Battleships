namespace Battleships.Core
{
    public class BoardInitializer
    {
        private readonly FleetPositioner _fleetPositioner;
        private readonly FleetConfigurationProvider _fleetConfigurationProvider;

        internal BoardInitializer(FleetPositioner fleetPositioner, FleetConfigurationProvider fleetConfigurationProvider)
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
