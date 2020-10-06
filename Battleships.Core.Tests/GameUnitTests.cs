using Battleships.Core;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Core.Tests
{
    internal class GameUnitTests
    {
        private Game _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new Game();
        }

        [Test]
        public void ShouldReturnOpponentsShotResult()
        {
            var shotResult = new ShotResult(); 
            var opponentDouble = A.Fake<Board>();

            A.CallTo(() => opponentDouble.Check(null)).Returns(shotResult);

            _subject.Shoot(opponentDouble).Should().Be(shotResult);
        }
    }
}