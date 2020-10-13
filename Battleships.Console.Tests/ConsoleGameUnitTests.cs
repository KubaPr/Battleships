using Battleships.Console;
using Battleships.Core;
using FakeItEasy;
using NUnit.Framework;

namespace Battleships.Tests
{
    public class Tests
    {
        private ConsoleGame _subject;
        private BoardInitializer _boardInitializerDouble;
        private ConsoleWrapper _consoleWrapperDouble;
        private BoardPrinter _boardPrinterDouble;
        private ConsoleCoordinatesReader _consoleCoordinateReaderDouble;
        private CoordinatesMapper _inputMapperDouble;
        private ShotResultMapper _shotResultMapperDouble;

        [SetUp]
        public void Setup()
        {
            _boardInitializerDouble = A.Fake<BoardInitializer>();
            _consoleWrapperDouble = A.Fake<ConsoleWrapper>();
            _boardPrinterDouble = A.Fake<BoardPrinter>();
            _consoleCoordinateReaderDouble = A.Fake<ConsoleCoordinatesReader>();
            _inputMapperDouble = A.Fake<CoordinatesMapper>();
            _shotResultMapperDouble = A.Fake<ShotResultMapper>();

            _subject = new ConsoleGame(
                _boardInitializerDouble,
                _consoleWrapperDouble,
                _boardPrinterDouble,
                _consoleCoordinateReaderDouble,
                _inputMapperDouble,
                _shotResultMapperDouble);
        }

        [Test]
        public void ShouldPrintBoard()
        {
            const string board = "board";

            A.CallTo(() => _boardPrinterDouble.Print(A<Board>._)).Returns(board);
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _consoleWrapperDouble.Print(board)).MustHaveHappened();
        }

        [Test]
        public void ShouldGetPlayerInput()
        {
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _consoleCoordinateReaderDouble.ReadInput()).MustHaveHappened();
        }

        [Test]
        public void ShouldMapPlayerInputToCoordinates()
        {
            const string input = "input";

            A.CallTo(() => _consoleCoordinateReaderDouble.ReadInput()).Returns(input);
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _inputMapperDouble.Map(input)).MustHaveHappened();
        }

        [Test]
        public void ShouldCheckCoordinatesOnTheBoard()
        {
            var coordinates = new Coordinates(0, 0);
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => _inputMapperDouble.Map(A<string>._)).Returns(coordinates);
            StubBoardIsConquered(boardDouble);

            _subject.Start();

            A.CallTo(() => boardDouble.Check(coordinates)).MustHaveHappened();
        }

        [Test]
        public void ShouldMapShotResult()
        {
            var shotResult = new ShotResult();
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => boardDouble.Check(A<Coordinates>._)).Returns(shotResult);
            StubBoardIsConquered(boardDouble);

            _subject.Start();

            A.CallTo(() => _shotResultMapperDouble.Map(shotResult)).MustHaveHappened();
        }

        [Test]
        public void ShouldPrintMappedShotResult()
        {
            const string mappedResult = "result";

            A.CallTo(() => _shotResultMapperDouble.Map(A<ShotResult>._)).Returns(mappedResult);
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _consoleWrapperDouble.Print(mappedResult)).MustHaveHappened();
        }

        [Test]
        public void WhenBoardIsConquered_ShouldReturnGameOverMessage()
        {
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => boardDouble.IsConquered).Returns(true);

            _subject.Start();

            A.CallTo(() => _consoleWrapperDouble.Print("You won! Game Over!")).MustHaveHappened();
        }

        private Board CreateBoardDouble()
        {
            var boardDouble = A.Fake<Board>();

            A.CallTo(() => _boardInitializerDouble.Initialize()).Returns(boardDouble);

            return boardDouble;
        }

        private void StubBoardIsConquered(Board boardDouble = null)
        {
            var board = boardDouble ?? CreateBoardDouble();

            A.CallTo(() => board.IsConquered).Returns(true);
            A.CallTo(() => board.IsConquered).Returns(false).Twice();
        }
    }
}