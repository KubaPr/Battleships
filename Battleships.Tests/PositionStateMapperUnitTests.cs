using Battleships.Core;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Tests
{
    internal class PositionStateMapperUnitTests
    {
        [TestCase(PositionState.Hit, "X")]
        [TestCase(PositionState.Miss, "O")]
        [TestCase(PositionState.Unchecked, "~")]
        public void ShouldMapPositionStates(PositionState state, string consoleState)
        {
            new PositionStateMapper().Map(state).Should().Be(consoleState);
        }
    }
}
