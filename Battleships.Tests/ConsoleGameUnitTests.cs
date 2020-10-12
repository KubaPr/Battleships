using Battleships.Core;
using Battleships.IoC;
using FakeItEasy;
using NUnit.Framework;

namespace Battleships.Tests
{
    public class Tests
    {
        private ConsoleGame _subject;
        private BoardInitializerFactory _boardInitializerFactoryDouble;
        private ConsolePrinter _consolePrinterDouble;
        private BoardPrinter _boardPrinterDouble;
        private ConsoleReader _consoleReaderDouble;
        private InputMapper _inputMapperDouble;
        private ShotResultMapper _shotResultMapperDouble;

        [SetUp]
        public void Setup()
        {
            _boardInitializerFactoryDouble = A.Fake<BoardInitializerFactory>();
            _consolePrinterDouble = A.Fake<ConsolePrinter>();
            _boardPrinterDouble = A.Fake<BoardPrinter>();
            _consoleReaderDouble = A.Fake<ConsoleReader>();
            _inputMapperDouble = A.Fake<InputMapper>();
            _shotResultMapperDouble = A.Fake<ShotResultMapper>();

            _subject = new ConsoleGame(
                _boardInitializerFactoryDouble,
                _consolePrinterDouble,
                _boardPrinterDouble,
                _consoleReaderDouble,
                _inputMapperDouble,
                _shotResultMapperDouble);
        }

        [Test]
        public void ShouldCreateBoardInitializer()
        {
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _boardInitializerFactoryDouble.CreateBoardInitializer()).MustHaveHappened();
        }

        //[Test]
        //public void ShouldInitializeBoard()
        //{
        //    var initializerDouble = A.Fake<BoardInitializer>();

        //    A.CallTo(() => _boardInitializerFactoryDouble.CreateBoardInitializer()).Returns(initializerDouble);
        //    StubBoardIsConquered();

        //    _subject.Start();

        //    A.CallTo(() => initializerDouble.Initialize()).MustHaveHappened();
        //}

        [Test]
        public void ShouldPrintBoard()
        {
            const string board = "board";

            A.CallTo(() => _boardPrinterDouble.Print(A<Board>._)).Returns(board);
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _consolePrinterDouble.Print(board)).MustHaveHappened();
        }

        [Test]
        public void ShouldGetPlayerInput()
        {
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _consoleReaderDouble.ReadInput()).MustHaveHappened();
        }

        [Test]
        public void ShouldMapPlayerInputToCoordinates()
        {
            const string input = "input";

            A.CallTo(() => _consoleReaderDouble.ReadInput()).Returns(input);
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
            Board boardDouble = CreateBoardDouble();

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

            A.CallTo(() => _consolePrinterDouble.Print(mappedResult)).MustHaveHappened();
        }

        [Test]
        public void WhileBoardIsNotConquered_ShouldCheckCoordinatesOnTheBoard()
        {
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => boardDouble.IsConquered).Returns(true);
            A.CallTo(() => boardDouble.IsConquered).Returns(false).Twice();

            _subject.Start();

            A.CallTo(() => boardDouble.Check(A<Coordinates>._)).MustHaveHappenedTwiceExactly();
        }

        private Board CreateBoardDouble()
        {
            var boardDouble = A.Fake<Board>();
            var initializerDouble = A.Fake<BoardInitializer>();

            A.CallTo(() => _boardInitializerFactoryDouble.CreateBoardInitializer()).Returns(initializerDouble);
            A.CallTo(() => initializerDouble.Initialize()).Returns(boardDouble);

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