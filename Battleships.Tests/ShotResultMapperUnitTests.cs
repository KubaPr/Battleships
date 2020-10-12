using Battleships.Core;
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
        public void WhenResultIsHit_ShouldReturnMessageWithNumberOfMastsOfHitShip()
        {
            _subject.Map(new ShotResult(new Ship(2))).Should().Be("You hit 2-masted ship!");
        }

        [Test]
        public void WhenResultIsNotHit_ShouldReturnMissedMessage()
        {
            _subject.Map(new ShotResult()).Should().Be("Miss!");
        }
    }
}
