using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Battleships.Core.Tests
{
    internal class ShipPositionerUnitTests
    {
        private ShipPositioner _subject;
        private RandomPositioner randomPositionerDouble;

        [SetUp]
        public void SetUp()
        {
            randomPositionerDouble = A.Fake<RandomPositioner>();
            _subject = new ShipPositioner(randomPositionerDouble);
        }

        [Test]
        public void ShouldPutOneFiveMastedShipRandomly()
        {
            var fiveMastedShipPositions = new List<Position> { new Position(new Coordinates(0, 0)) };

            A.CallTo(() => randomPositionerDouble
                .GeneratePositionsForShip(A<Ship>.That.Matches(ship => ship.NumberOfMasts == 5)))
                .Returns(fiveMastedShipPositions);

            _subject.CreatePositions().Should().Contain(fiveMastedShipPositions).And.HaveCount(1);
        }

        [Test]
        public void ShouldPutTwoFourMastedShipsRandomly()
        {
            var fourMastedShipPositions = new List<Position> { new Position(new Coordinates(0, 0)) };
            var anotherFourMastedShipPositions = new List<Position> { new Position(new Coordinates(9, 2)) };

            A.CallTo(() => randomPositionerDouble
                .GeneratePositionsForShip(A<Ship>.That.Matches(ship => ship.NumberOfMasts == 4)))
                .Returns(fourMastedShipPositions).Once();
            A.CallTo(() => randomPositionerDouble
                .GeneratePositionsForShip(A<Ship>.That.Matches(ship => ship.NumberOfMasts == 4)))
                .Returns(anotherFourMastedShipPositions).Once();

            _subject.CreatePositions().Should().Contain(fourMastedShipPositions)
                .And.Contain(anotherFourMastedShipPositions)
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
        public void WhenShipsAreNotAtLeastOnePositionAway_ShouldReposition(
            byte firstPositionVertical,
            byte firstPositionHorizontal,
            byte secondPositionVertical,
            byte secondPositionHorizontal)
        {
            const byte numberOfShipsOnTheBoard = 3;

            var positions = new List<Position> { new Position(
                new Coordinates(firstPositionVertical, firstPositionHorizontal)) };
            var tooClosePositions = new List<Position> { new Position(
                new Coordinates(secondPositionVertical, secondPositionHorizontal)) };

            A.CallTo(() => randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(positions).Once();
            A.CallTo(() => randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(tooClosePositions).Once();

            _subject.CreatePositions();

            A.CallTo(() => randomPositionerDouble.GeneratePositionsForShip(A<Ship>._))
                .MustHaveHappened(numberOfShipsOnTheBoard + 1, Times.Exactly);
         }

        [TestCase(0, 0, 0, 2)]
        [TestCase(0, 0, 2, 0)]
        [TestCase(9, 9, 9, 7)]
        [TestCase(9, 9, 7, 9)]
        public void WhenShipsAreAtLeastOnePositionAway_ShouldNotReposition(
            byte firstPositionVertical,
            byte firstPositionHorizontal,
            byte secondPositionVertical,
            byte secondPositionHorizontal)
        {
            const byte numberOfShipsOnTheBoard = 3;

            var positions = new List<Position> { new Position(
                new Coordinates(firstPositionVertical, firstPositionHorizontal)) };
            var tooClosePositions = new List<Position> { new Position(
                new Coordinates(secondPositionVertical, secondPositionHorizontal)) };

            A.CallTo(() => randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(positions).Once();
            A.CallTo(() => randomPositionerDouble.GeneratePositionsForShip(A<Ship>._)).Returns(tooClosePositions).Once();

            _subject.CreatePositions();

            A.CallTo(() => randomPositionerDouble.GeneratePositionsForShip(A<Ship>._))
                .MustHaveHappened(numberOfShipsOnTheBoard, Times.Exactly);
        }
    }
}
