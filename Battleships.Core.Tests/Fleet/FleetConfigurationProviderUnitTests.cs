using Battleships.Core.Fleet;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Core.Tests.Fleet
{
    internal class FleetConfigurationProviderUnitTests
    {
        [Test]
        public void ShouldReturnOneDestroyerAndTwoBattleships()
        {
            var subject = new FleetConfigurationProvider();

            subject.Get().Should().BeEquivalentTo(new Destroyer(), new Battleship(), new Battleship());
        }
    }
}
