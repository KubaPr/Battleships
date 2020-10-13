using Battleships.Core.Fleet;

namespace Battleships.Core.Tests
{
    internal class DummyShip : Ship
    {
        public DummyShip(int numberOfMasts) : base(numberOfMasts)
        {
        }

        public override string Name => "Dummy";
    }
}
