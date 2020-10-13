using Battleships.Core.Fleet;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Core.Tests.Fleet
{
    internal class BattleshipUnitTests
    {
        [Test]
        public void ShouldBeShip()
        {
            new Battleship().Should().BeAssignableTo<Ship>();
        }

        [Test]
        public void ShouldHaveFourMasts()
        {
            new Battleship().NumberOfMasts.Should().Be(4);
        }
    }
}
