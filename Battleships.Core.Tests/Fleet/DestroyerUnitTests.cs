using Battleships.Core.Fleet;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Core.Tests.Fleet
{
    internal class DestsroyerUnitTests
    {
        [Test]
        public void ShouldBeShip()
        {
            new Destroyer().Should().BeAssignableTo<Ship>();
        }

        [Test]
        public void ShouldHaveFiveMasts()
        {
            new Destroyer().NumberOfMasts.Should().Be(5);
        }
    }
}
