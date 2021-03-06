﻿using Battleships.Core.Fleet;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Battleships.Core.Tests.Fleet
{
    internal class RandomPositionerUnitTests
    {
        private readonly Ship _dummyShip = new DummyShip(1);

        private RandomPositioner _subject;
        private RandomGenerator _randomGeneratorDouble;

        [SetUp]
        public void SetUp()
        {
            _randomGeneratorDouble = A.Fake<RandomGenerator>();

            _subject = new RandomPositioner(_randomGeneratorDouble);
        }

        [Test]
        public void WhenRandomBoolIsTrue_ShouldPlaceShipHorizontally()
        {
            const int initialCoordinate = 0;

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomBool()).Returns(true);
            A.CallTo(() => _randomGeneratorDouble.GenerateRandomNumber(A<int>._, A<int>._)).Returns(initialCoordinate);

            _subject.GeneratePositionsForShip(new DummyShip(3)).Select(position => position.Coordinates.Horizontal).
                Should().BeEquivalentTo(initialCoordinate, initialCoordinate + 1, initialCoordinate + 2);
        }

        [Test]
        public void WhenRandomBoolIsFalse_ShouldPlaceShipVertically()
        {
            const int initialCoordinate = 0;

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomBool()).Returns(false);
            StubRandomNumberGeneration(initialCoordinate);

            _subject.GeneratePositionsForShip(new DummyShip(3)).Select(position => position.Coordinates.Vertical).
                Should().BeEquivalentTo(initialCoordinate, initialCoordinate + 1, initialCoordinate + 2);
        }

        [Test]
        public void WhenPlacingShipHorizontally_ShouldGenerateVerticalInitialPositionBetweenZeroAndBoardSizeMinusOne()
        {
            A.CallTo(() => _randomGeneratorDouble.GenerateRandomBool()).Returns(true);

            _subject.GeneratePositionsForShip(_dummyShip);

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomNumber(0, Board.Size - 1)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void WhenPlacingShipHorizontally_ShouldGenerateHorizontalInitialPositionBetweenZeroAndBoardSizeMinusOneMinusShipSize()
        {
            var ship = new DummyShip(4);

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomBool()).Returns(true);

            _subject.GeneratePositionsForShip(ship);

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomNumber(0, Board.Size - 1 - ship.NumberOfMasts))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void WhenPlacingShipVertically_ShouldGenerateHorizontalInitialPositionBetweenZeroAndBoardSizeMinusOne()
        {
            A.CallTo(() => _randomGeneratorDouble.GenerateRandomBool()).Returns(false);

            _subject.GeneratePositionsForShip(_dummyShip);

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomNumber(0, Board.Size - 1)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void WhenPlacingShipVertically_ShouldGenerateVerticalInitialPositionBetweenZeroAndBoardSizeMinusOneMinusShipSize()
        {
            var ship = new DummyShip(4);

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomBool()).Returns(false);

            _subject.GeneratePositionsForShip(ship);

            A.CallTo(() => _randomGeneratorDouble.GenerateRandomNumber(0, Board.Size - 1 - ship.NumberOfMasts))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldReturnShipWithAllPositions()
        {
            var ship = new DummyShip(4);

            _subject.GeneratePositionsForShip(ship).Select(position => position.Occupant).Distinct().Single()
                .Should().Be(ship);
        }

        [Test]
        public void ShouldReturnAsManyPositionsAsMasts()
        {
            var ship = new DummyShip(3);

            StubRandomNumberGeneration(6);

            _subject.GeneratePositionsForShip(ship).Should().HaveCount(ship.NumberOfMasts);
        }

        [Test]
        public void ShouldNotAcceptShipsBiggerThanTheBoard()
        {
            var subject = new RandomPositioner(_randomGeneratorDouble);

            Action act = () => subject.GeneratePositionsForShip(new DummyShip(Board.Size + 1));

            act.Should().Throw<ArgumentException>().WithMessage("Ship cannot be bigger than the board");
        }

        private void StubRandomNumberGeneration(int randomNumber)
        {
            A.CallTo(() => _randomGeneratorDouble.GenerateRandomNumber(A<int>._, A<int>._)).Returns(randomNumber);
        }
    }
}
