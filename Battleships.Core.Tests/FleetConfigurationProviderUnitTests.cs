using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Core.Tests
{
    internal class FleetConfigurationProviderUnitTests
    {
        [Test]
        public void ShouldReturnOneFiveMastedShipAndTwoFourMastedShips()
        {
            var subject = new FleetConfigurationProvider();

            subject.Get().Should().BeEquivalentTo(new Ship(5), new Ship(4), new Ship(4));
        }
    }
}
