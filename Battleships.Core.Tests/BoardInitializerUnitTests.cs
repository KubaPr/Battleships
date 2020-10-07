using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Battleships.Core.Tests
{
    internal class BoardInitializerUnitTests
    {
        private BoardInitializer _subject;
        private ShipPositioner _shipPositionerDouble;

        [SetUp]
        public void SetUp()
        {
            _shipPositionerDouble = A.Fake<ShipPositioner>();

            _subject = new BoardInitializer(_shipPositionerDouble);
        }

        [Test]
        public void ShouldReturnBoard()
        {
            _subject.Initialize().Should().BeOfType<Board>();
        }

        [Test]
        public void ShouldGetPositions()
        {
            _subject.Initialize();

            A.CallTo(() => _shipPositionerDouble.CreatePositions()).MustHaveHappened();
        }

        [Test]
        public void ShouldPutPositionsOnBoard()
        {
            var coordinates = new Coordinates(5, 5);
            var ship = new Ship(1);

            A.CallTo(() => _shipPositionerDouble.CreatePositions()).Returns(
                new List<Position>
                {
                    new Position(coordinates, ship)
                });

            _subject.Initialize().Check(coordinates).Ship.Should().Be(ship);
        }
    }
}
