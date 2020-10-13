using Battleships.Console;
using Battleships.Core;
using Battleships.Core.Fleet;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Tests
{
    internal class ShotResultMapperUnitTests
    {
        private ShotResultMapper _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new ShotResultMapper();
        }

        [Test]
        public void WhenResultIsHit_ShouldReturnMessageWithShipName()
        {
            var ship = new Destroyer();

            _subject.Map(new ShotResult(ship)).Should().Be($"You hit a {ship.Name}!");
        }

        [Test]
        public void WhenResultIsNotHit_ShouldReturnMissedMessage()
        {
            _subject.Map(new ShotResult()).Should().Be("Miss!");
        }
    }
}
