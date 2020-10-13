using Battleships.Core;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Battleships.Tests
{
    internal class BoardPrinterUnitTests
    {
        private BoardPrinter _subject;
        private PositionStateMapper _positionStateMapperDouble;

        [SetUp]
        public void SetUp()
        {
            _positionStateMapperDouble = A.Fake<PositionStateMapper>();

            _subject = new BoardPrinter(_positionStateMapperDouble);
        }

        [Test]
        public void ShouldPrintBoardHeaderAsFirstRow()
        {
            var boardDouble = CreateDummyBoard();

            _subject.Print(boardDouble).Should().StartWith("  A B C D E F G H I J");
        }

        [Test]
        public void ShouldPrintHeaderAndTenRowsSeparatedByNewLine()
        {
            Board boardDouble = CreateDummyBoard();

            _subject.Print(boardDouble).Split(Environment.NewLine).Should().HaveCount(Board.Size + 1);
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
        public void ShouldEachBoardRowContainTenMappedPositionStatesSeparatedBySpace(int rowNumber)
        {
            const string state = "state";
            const int headerOffset = 1;

            var boardDouble = CreateDummyBoard();
            A.CallTo(() => _positionStateMapperDouble.Map(A<PositionState>._)).Returns(state);

            _subject.Print(boardDouble).Split(Environment.NewLine)[rowNumber + headerOffset]
                .Should().Contain(' ' + state, Exactly.Times(Board.Size));
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
        public void ShouldEachBoardRowStartWithRowNumber(int rowNumber)
        {
            const string state = "state";
            const int headerOffset = 1;

            var boardDouble = CreateDummyBoard();
            A.CallTo(() => _positionStateMapperDouble.Map(A<PositionState>._)).Returns(state);

            _subject.Print(boardDouble).Split(Environment.NewLine)[rowNumber + headerOffset]
                .Should().StartWith(rowNumber.ToString());
        }

        private Board CreateDummyBoard()
        {
            var boardDouble = A.Fake<Board>();

            A.CallTo(() => boardDouble.Positions).Returns(CreateAllBoardPositions());

            return boardDouble;
        }

        private List<Position> CreateAllBoardPositions()
        {
            var positions = new List<Position>();

            for (var horizontal = 0; horizontal < Board.Size; horizontal++)
            {
                for (var vertical = 0; vertical < Board.Size; vertical++)
                {
                    positions.Add(new Position(new Coordinates(vertical, horizontal)));
                }
            }

            return positions;
        }
    }
}
