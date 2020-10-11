using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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

            _subject.CreatePositions(new List<Ship> { new Ship(1), new Ship(2) }).Should()
                .Contain(firstShipPositions).And
                .Contain(anotherShipPositions);
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

        [Test]
        public void ShouldReturnUnoccupiedPositionsWhereThereAreNoShips()
        {
            var positions = _subject.CreatePositions(new List<Ship> { new Ship(1) });

            positions.Where(position => !position.IsOccupied).Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ShouldReturnAsManyPositionsAsOnGameBoard()
        {
            var positions = _subject.CreatePositions(new List<Ship> { new Ship(4), new Ship(2) });

            positions.Should().HaveCount(Board.Size * Board.Size);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void ShouldNotDuplicatePositions(int coordinate)
        {
            A.CallTo(() => _randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(
                new List<Position>
                {
                    new Position(new Coordinates(1, 8)),
                    new Position(new Coordinates(1, 9))
                });

            var positions = _subject.CreatePositions(new List<Ship> { new Ship(2) });

            //TODO: Go back to this and think if this could be more readable
            positions.Where(position => position.Coordinates.Vertical == coordinate)
                .Select(position => position.Coordinates.Horizontal)
                .Should().OnlyHaveUniqueItems();
            positions.Where(position => position.Coordinates.Horizontal == coordinate)
                .Select(position => position.Coordinates.Vertical)
                .Should().OnlyHaveUniqueItems();
        }
    }
}
