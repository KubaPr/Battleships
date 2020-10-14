using Battleships.Core;

namespace Battleships.Console
{
    public class ConsoleGame
    {
        private readonly ConsoleWrapper _consoleWrapper;
        private readonly BoardPrinter _boardPrinter;
        private readonly ConsoleCoordinatesReader _consoleCoordinateReader;
        private readonly CoordinatesMapper _coordinatesMapper;
        private readonly ShotResultMapper _shotResultMapper;
        private readonly BoardInitializer _boardInitializer;

        internal ConsoleGame(
            BoardInitializer boardInitializer,
            ConsoleWrapper consoleWrapper,
            BoardPrinter boardPrinter,
            ConsoleCoordinatesReader consoleReader,
            CoordinatesMapper coordinatesMapper,
            ShotResultMapper shotResultMapper)
        {
            _boardInitializer = boardInitializer;
            _consoleWrapper = consoleWrapper;
            _boardPrinter = boardPrinter;
            _consoleCoordinateReader = consoleReader;
            _coordinatesMapper = coordinatesMapper;
            _shotResultMapper = shotResultMapper;
        }

        public void Start()
        {
            var board = _boardInitializer.Initialize();

            ShowGameStartMessage();

            while (!board.IsConquered)
            {
                var coordinates = GetCoordinates();
                var shotResult = board.Check(coordinates);
                _consoleWrapper.Clear();
                ShowShotResult(shotResult);
                ShowBoard(board);
            }

            ShowGameOverMessage();
        }

        private void ShowGameStartMessage()
        {
            _consoleWrapper.Print(
                "Shot between A0 and J9 to start a new game! Two Destroyers (4 masts) and a Battleship (5 masts) are already positioned.");
        }

        private Coordinates GetCoordinates()
        {
            var inputCoordinates = _consoleCoordinateReader.ReadInput();

            return _coordinatesMapper.Map(inputCoordinates);
        }

        private void ShowShotResult(ShotResult shotResult)
        {
            _consoleWrapper.Print(_shotResultMapper.Map(shotResult));
        }

        private void ShowBoard(Board board)
        {
            _consoleWrapper.Print(_boardPrinter.Print(board));
        }

        private void ShowGameOverMessage()
        {
            _consoleWrapper.Print("You won! Game Over!");
        }
    }
}
