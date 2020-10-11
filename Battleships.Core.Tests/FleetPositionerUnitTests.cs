using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Battleships.Core.Tests
{
    internal class FleetPositionerUnitTests
    {
        private FleetPositioner _subject;
        private RandomPositioner _randomPositionerDouble;

        [SetUp]
        public void SetUp()
        {
            _randomPositionerDouble = A.Fake<RandomPositioner>();

            _subject = new FleetPositioner(_randomPositionerDouble);
        }

        [Test]
        public void ShouldReturnRandomlyGeneratedPositionsForAllShips()
        {
            var firstShipPositions = new List<Position> { new Position(new Coordinates(0, 0)) };
            var anotherShipPositions = new List<Position> { new Position(new Coordinates(9, 2)) };

            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._))
                .Returns(firstShipPositions).Once();
            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._))
                .Returns(anotherShipPositions).Once();

            _subject.CreatePositions(new List<Ship> { new Ship(1), new Ship(2) }).Should().Contain(firstShipPositions)
                .And.Contain(anotherShipPositions)
                .And.HaveCount(2);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(9, 9, 9, 9)]
        [TestCase(9, 9, 9, 8)]
        [TestCase(9, 9, 8, 9)]
        [TestCase(9, 9, 8, 8)]
        public void WhenShipIsNotAtLeastOnePositionAwayFromOthers_ShouldReposition(
            int firstPositionVertical,
            int firstPositionHorizontal,
            int secondPositionVertical,
            int secondPositionHorizontal)
        {
            const int numberOfShipsOnTheBoard = 3;

            var positions = new List<Position> { new Position(
                new Coordinates(firstPositionVertical, firstPositionHorizontal)) };
            var tooClosePositions = new List<Position> { new Position(
                new Coordinates(secondPositionVertical, secondPositionHorizontal)) };

            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(positions).Once();
            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(tooClosePositions).Once();

            _subject.CreatePositions(new List<Ship> { new Ship(1), new Ship(2), new Ship(3) });

            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._))
                .MustHaveHappened(numberOfShipsOnTheBoard + 1, Times.Exactly);
         }

        [TestCase(0, 0, 0, 2)]
        [TestCase(0, 0, 2, 0)]
        [TestCase(9, 9, 9, 7)]
        [TestCase(9, 9, 7, 9)]
        public void WhenShipIsAtLeastOnePositionAwayFromOthers_ShouldNotReposition(
            int firstPositionVertical,
            int firstPositionHorizontal,
            int secondPositionVertical,
            int secondPositionHorizontal)
        {
            const int numberOfShipsOnTheBoard = 3;

            var positions = new List<Position> { new Position(
                new Coordinates(firstPositionVertical, firstPositionHorizontal)) };
            var tooClosePositions = new List<Position> { new Position(
                new Coordinates(secondPositionVertical, secondPositionHorizontal)) };

            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(positions).Once();
            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(tooClosePositions).Once();

            _subject.CreatePositions(new List<Ship> { new Ship(1), new Ship(2), new Ship(3) });

            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._))
                .MustHaveHappened(numberOfShipsOnTheBoard, Times.Exactly);
        }
    }
}
