using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Tests
{
    internal class CoordinatesMapperUnitTests
    {
        private CoordinatesMapper _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new CoordinatesMapper();
        }

        [TestCase("A", 0)]
        [TestCase("B", 1)]
        [TestCase("C", 2)]
        [TestCase("D", 3)]
        [TestCase("E", 4)]
        [TestCase("F", 5)]
        [TestCase("G", 6)]
        [TestCase("H", 7)]
        [TestCase("I", 8)]
        [TestCase("J", 9)]
        public void ShouldMapVerticalCoordinatesToIntegerEquivalents(string stringCoordinate, int coordinate)
        {
            var input = $"{stringCoordinate}{default(int)}";

            _subject.Map(input).Vertical.Should().Be(coordinate);
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        [TestCase("4", 4)]
        [TestCase("5", 5)]
        [TestCase("6", 6)]
        [TestCase("7", 7)]
        [TestCase("8", 8)]
        [TestCase("9", 9)]
        public void ShouldMapHorizontalCoordinates(string stringCoordinate, int coordinate)
        {
            var input = $"G{stringCoordinate}";

            _subject.Map(input).Horizontal.Should().Be(coordinate);
        }
    }
}
