using Battleships.Core.Fleet;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Battleships.Core.Tests
{
    internal class BoardUnitTests
    {
        [Test]
        public void ShouldReturnShotResult()
        {
            var dummyCoordinates = new Coordinates(0, 0);

            var subject = CreateBoard(CreatePosition(dummyCoordinates));

            subject.Check(dummyCoordinates).Should().BeOfType<ShotResult>();
        }

        [Test]
        public void WhenPositionIsNotOccupied_ShouldHitResultBeFalse()
        {
            var unoccupiedCoordinates = new Coordinates(0, 0);

            var subject = CreateBoard(CreatePosition(unoccupiedCoordinates, occupant: null));

            subject.Check(unoccupiedCoordinates).IsHit.Should().BeFalse();
        }

        [Test]
        public void WhenPositionIsOccupied_ShouldHitResultBeTrue()
        {
            var occupiedCoordinates = new Coordinates(0, 0);

            var subject = CreateBoard(CreatePosition(occupiedCoordinates, occupant: new DummyShip(1)));

            subject.Check(occupiedCoordinates).IsHit.Should().BeTrue();
        }

        [Test]
        public void WhenPositionIsOccupied_ShouldReturnOccupant()
        {
            var ship = new DummyShip(1);
            var occupiedCoordinates = new Coordinates(0, 0);

            var subject = CreateBoard(CreatePosition(occupiedCoordinates, occupant: ship));

            subject.Check(occupiedCoordinates).Ship.Should().Be(ship);
        }

        [Test]
        public void ShouldMarkPositionAsChecked()
        {
            var coordinates = new Coordinates(0, 0);
            var positionDouble = CreatePosition(coordinates);

            CreateBoard(positionDouble).Check(coordinates);

            A.CallTo(() => positionDouble.MarkAsChecked()).MustHaveHappened();
        }

        private Board CreateBoard(Position position)
        {
            return new Board(new List<Position> { position });
        }

        private static Position CreatePosition(Coordinates coordinates, Ship occupant = null)
        {
            return A.Fake<Position>(opt => opt.WithArgumentsForConstructor(() => new Position(coordinates, occupant)));
        }
    }
}
