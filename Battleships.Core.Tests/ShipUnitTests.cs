using FluentAssertions;
using NUnit.Framework;
using System;

namespace Battleships.Core.Tests
{
    internal class ShipUnitTests
    {
        [Test]
        public void ShouldHaveAtLeastOneMast()
        {
            Action act = () => new Ship(0);

            act.Should().Throw<ArgumentException>().WithMessage("Ship must have at least one mast.");
        }

        [Test]
        public void WhenAllMastsHit_ShouldBeSunk()
        {
            var subject = new Ship(4);

            subject.ReportHit();
            subject.ReportHit();
            subject.ReportHit();
            subject.ReportHit();

            subject.IsSunk.Should().BeTrue();
        }
    }
}
