using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Battleships.Core.Tests
{
    internal class BoardComponentTests
    {
        [Test]
        public void WhenNotAllShipsAreSunk_ShouldNotBeConquered()
        {
            var firstShot = new Coordinates(0, 0);
            var subject = new Board(
                new List<Position>
                {
                    new Position(firstShot, new DummyShip(1)),
                    new Position(new Coordinates(1, 1), new DummyShip(1))
                });

            subject.Check(firstShot);

            subject.IsConquered.Should().BeFalse();
        }

        [Test]
        public void WhenAllShipsAreSunk_ShouldBeConquered()
        {
            var firstShot = new Coordinates(0, 0);
            var secondShot = new Coordinates(1, 1);
            var thirdShot = new Coordinates(1, 2);
            var doubleMastedShip = new DummyShip(2);

            var subject = new Board(
                new List<Position>
                {
                    new Position(new Coordinates(0,1), null),
                    new Position(firstShot, new DummyShip(1)),
                    new Position(secondShot, doubleMastedShip),
                    new Position(thirdShot, doubleMastedShip)
                });

            subject.Check(firstShot);
            subject.Check(secondShot);
            subject.Check(thirdShot);

            subject.IsConquered.Should().BeTrue();
        }
    }
}
