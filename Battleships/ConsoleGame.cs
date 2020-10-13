using Battleships.Core;

namespace Battleships
{
    internal class ConsoleGame : Game
    {
        private readonly ConsoleWrapper _consoleWrapper;
        private readonly BoardPrinter _boardPrinter;
        private readonly ConsoleCoordinatesReader _consoleCoordinateReader;
        private readonly CoordinatesMapper _coordinatesMapper;
        private readonly ShotResultMapper _shotResultMapper;

        public ConsoleGame(
            BoardInitializer boardInitializer,
            ConsoleWrapper consoleWrapper,
            BoardPrinter boardPrinter,
            ConsoleCoordinatesReader consoleReader,
            CoordinatesMapper coordinatesMapper,
            ShotResultMapper shotResultMapper) : base(boardInitializer)
        {
            _consoleWrapper = consoleWrapper;
            _boardPrinter = boardPrinter;
            _consoleCoordinateReader = consoleReader;
            _coordinatesMapper = coordinatesMapper;
            _shotResultMapper = shotResultMapper;
        }

        public override Coordinates GetCoordinates()
        {
            var inputCoordinates = _consoleCoordinateReader.ReadInput();

            return _coordinatesMapper.Map(inputCoordinates);
        }

        public override void ShowBoard(Board board)
        {
            _consoleWrapper.Print(_boardPrinter.Print(board));
        }

        public override void ShowGameOverMessage()
        {
            _consoleWrapper.Print("You won! Game Over!");
        }

        public override void ShowShotResult(ShotResult shotResult)
        {
            _consoleWrapper.Clear();
            _consoleWrapper.Print(_shotResultMapper.Map(shotResult));
        }
    }
}
