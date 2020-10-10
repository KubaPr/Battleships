namespace Battleships.Core
{
    internal class BoardInitializer
    {
        private readonly ShipPositioner _shipPositioner;

        public BoardInitializer(ShipPositioner shipPositioner)
        {
            _shipPositioner = shipPositioner;
        }

        public Board Initialize()
        {
            return new Board(_shipPositioner.CreatePositions());
        }
    }
}
