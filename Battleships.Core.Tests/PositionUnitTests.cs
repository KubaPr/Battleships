using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Battleships.Core.Tests
{
    internal class PositionUnitTests
    {
        private readonly Coordinates DummyCoordinates = new Coordinates(0, 0);

        [Test]
        public void ShouldAlwaysHaveCoordinates()
        {
            Action act = () => new Position(null, null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void WhenHasOccupant_ShouldBeOccupied()
        {
            new Position(DummyCoordinates, new Ship(1)).IsOccupied.Should().BeTrue();
        }

        [Test]
        public void WhenDoesNotHaveOccupant_ShouldNotBeOccupied()
        {
            new Position(DummyCoordinates, null).IsOccupied.Should().BeFalse();
        }

        [Test]
        public void ShouldPositionStateBeInitializedAsUnchecked()
        {
            new Position(DummyCoordinates, null).State.Should().Be(PositionState.Unchecked);
        }

        [Test]
        public void WhenMarkingAsChecked_AndHasOccupant_ShouldStateBeHit()
        {
            var subject = new Position(DummyCoordinates, CreateFakeShip());

            subject.MarkAsChecked();

            subject.State.Should().Be(PositionState.Hit);
        }

        [Test]
        public void WhenMarkingAsChecked_AndDoesNotHaveOccupant_ShouldStateBeMissed()
        {
            var subject = new Position(DummyCoordinates, occupant: null);

            subject.MarkAsChecked();

            subject.State.Should().Be(PositionState.Miss);
        }

        [Test]
        public void WhenMarkingAsChecked_AndHasOccupant_ShouldReportHitToOccupant()
        {
            var occupantDouble = CreateFakeShip();
            var subject = new Position(DummyCoordinates, occupantDouble);

            subject.MarkAsChecked();

            A.CallTo(() => occupantDouble.ReportHit()).MustHaveHappened();
        }

        [Test]
        public void WhenAlreadyMarkedAsChecked_AndHasOccupant_ShouldNotReportHitToOccupant()
        {
            var occupantDouble = CreateFakeShip();
            var subject = new Position(DummyCoordinates, occupantDouble);

            subject.MarkAsChecked();

            Fake.ClearRecordedCalls(occupantDouble);

            subject.MarkAsChecked();

            A.CallTo(() => occupantDouble.ReportHit()).MustNotHaveHappened();
        }

        private static Ship CreateFakeShip()
        {
            return A.Fake<Ship>(opt => opt.WithArgumentsForConstructor(() => new Ship(1)));
        }
    }
}
