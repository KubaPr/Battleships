using FakeItEasy;
using NUnit.Framework;

namespace Battleships.Core.Tests
{
    internal class GameUnitTests
    {
        private Game _subject;
        private BoardInitializer _boardInitializerDouble;

        [SetUp]
        public void Setup()
        {
            _boardInitializerDouble = A.Fake<BoardInitializer>();

            _subject = A.Fake<DummyGame>(
                options => options.WithArgumentsForConstructor(() => new DummyGame(_boardInitializerDouble)));
        }

        [Test]
        public void ShouldGetCoordinates()
        {
            StubBoardIsConquered();

            _subject.Start();

            A.CallTo(() => _subject.GetCoordinates()).MustHaveHappened();
        }

        [Test]
        public void ShouldCheckCoordinatesOnTheBoard()
        {
            var coordinates = new Coordinates(0, 0);
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => _subject.GetCoordinates()).Returns(coordinates);
            StubBoardIsConquered(boardDouble);

            _subject.Start();

            A.CallTo(() => boardDouble.Check(coordinates)).MustHaveHappened();
        }

        [Test]
        public void ShouldShowShotResult()
        {
            var shotResult = new ShotResult();
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => boardDouble.Check(A<Coordinates>._)).Returns(shotResult);
            StubBoardIsConquered(boardDouble);

            _subject.Start();

            A.CallTo(() => _subject.ShowShotResult(shotResult)).MustHaveHappened();
        }

        [Test]
        public void ShouldShowBoard()
        {
            var boardDouble = CreateBoardDouble();

            StubBoardIsConquered(boardDouble);

            _subject.Start();

            A.CallTo(() => _subject.ShowBoard(boardDouble)).MustHaveHappened();
        }

        [Test]
        public void WhenBoardIsConquered_ShouldReturnGameOverMessage()
        {
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => boardDouble.IsConquered).Returns(true);

            _subject.Start();

            A.CallTo(() => _subject.ShowGameOverMessage()).MustHaveHappened();
        }

        [Test]
        public void WhileBoardIsNotConquered_ShouldRepeatOperations()
        {
            var boardDouble = CreateBoardDouble();

            A.CallTo(() => boardDouble.IsConquered).Returns(true);
            A.CallTo(() => boardDouble.IsConquered).Returns(false).Twice();

            _subject.Start();

            A.CallTo(() => boardDouble.Check(A<Coordinates>._)).MustHaveHappenedTwiceExactly();
            A.CallTo(() => _subject.GetCoordinates()).MustHaveHappenedTwiceExactly();
            A.CallTo(() => _subject.ShowShotResult(A<ShotResult>._)).MustHaveHappenedTwiceExactly();
            A.CallTo(() => _subject.ShowBoard(boardDouble)).MustHaveHappenedTwiceExactly();
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

    public class DummyGame : Game
    {
        public DummyGame(BoardInitializer boardInitializer) : base(boardInitializer)
        {
        }

        public override Coordinates GetCoordinates() => new Coordinates(0, 0);

        public override void ShowBoard(Board board)
        {
        }

        public override void ShowGameOverMessage()
        {
        }

        public override void ShowShotResult(ShotResult shotResult)
        {
        }
    }
}
