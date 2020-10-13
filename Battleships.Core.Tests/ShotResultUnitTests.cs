using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Core.Tests
{
    internal class ShotResultUnitTests
    {
        [Test]
        public void WhenCreatedWithShip_ShouldShotResultBeHit()
        {
            new ShotResult(new DummyShip(1)).IsHit.Should().BeTrue();
        }

        [Test]
        public void WhenCreateWithoutShip_ShouldShotResultNotBeHit()
        {
            new ShotResult().IsHit.Should().BeFalse();
        }
    }
}
