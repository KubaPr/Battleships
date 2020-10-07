using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
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
                .CreatePositionsForShip(A<Ship>.That.Matches(ship => ship.NumberOfMasts == 5)))
                .Returns(fiveMastedShipPositions);

            _subject.CreatePositions().Should().Contain(fiveMastedShipPositions).And.HaveCount(1);
        }

        [Test]
        public void ShouldPutTwoFourMastedShipsRandomly()
        {
            var fourMastedShipPositions = new List<Position> { new Position(new Coordinates(0, 0)) };
            var anotherFourMastedShipPositions = new List<Position> { new Position(new Coordinates(9, 2)) };

            A.CallTo(() => randomPositionerDouble
                .CreatePositionsForShip(A<Ship>.That.Matches(ship => ship.NumberOfMasts == 4)))
                .Returns(fourMastedShipPositions).Once();
            A.CallTo(() => randomPositionerDouble
                .CreatePositionsForShip(A<Ship>.That.Matches(ship => ship.NumberOfMasts == 4)))
                .Returns(anotherFourMastedShipPositions).Once();

            _subject.CreatePositions().Should().Contain(fourMastedShipPositions)
                .And.Contain(anotherFourMastedShipPositions)
                .And.HaveCount(2);
        }

        //TODO: test cases? Or maybe it can be tested better
        [Test]
        public void WhenShipsAreNotAtLeastOnePositionAway_ShouldReposition()
        {
            var positions = new List<Position> { new Position(new Coordinates(0, 0)) };
            var tooClosePositions = new List<Position> { new Position(new Coordinates(0, 1)) };

            A.CallTo(() => randomPositionerDouble.CreatePositionsForShip(A<Ship>._)).Returns(positions).Once();
            A.CallTo(() => randomPositionerDouble.CreatePositionsForShip(A<Ship>._)).Returns(tooClosePositions).Once();

            _subject.CreatePositions();

            //TODO: something about this magic number
            A.CallTo(() => randomPositionerDouble.CreatePositionsForShip(A<Ship>._)).MustHaveHappened(4, Times.Exactly);
         }
    }
}
