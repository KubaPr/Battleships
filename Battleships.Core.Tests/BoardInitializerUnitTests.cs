using Battleships.Core.Fleet;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Battleships.Core.Tests
{
    internal class BoardInitializerUnitTests
    {
        private BoardInitializer _subject;
        private FleetPositioner _shipPositionerDouble;
        private FleetConfigurationProvider _fleetConfigurationProviderDouble;

        [SetUp]
        public void SetUp()
        {
            _shipPositionerDouble = A.Fake<FleetPositioner>();
            _fleetConfigurationProviderDouble = A.Fake<FleetConfigurationProvider>();

            _subject = new BoardInitializer(_shipPositionerDouble, _fleetConfigurationProviderDouble);
        }

        [Test]
        public void ShouldReturnBoard()
        {
            _subject.Initialize().Should().BeOfType<Board>();
        }

        [Test]
        public void ShouldCreatePositionsForConfiguredFleet()
        {
            var configuredShips = new List<Ship> { new DummyShip(8), new DummyShip(1) };

            A.CallTo(() => _fleetConfigurationProviderDouble.Get()).Returns(configuredShips);

            _subject.Initialize();

            A.CallTo(() => _shipPositionerDouble.CreatePositions(A<List<Ship>>.That.IsSameSequenceAs(configuredShips)))
                .MustHaveHappened();
        }

        [Test]
        public void ShouldPutPositionsOnBoard()
        {
            var coordinates = new Coordinates(5, 5);
            var ship = new DummyShip(1);

            A.CallTo(() => _shipPositionerDouble.CreatePositions(A<List<Ship>>._)).Returns(
                new List<Position>
                {
                    new Position(coordinates, ship)
                });

            _subject.Initialize().Check(coordinates).Ship.Should().Be(ship);
        }
    }
}
