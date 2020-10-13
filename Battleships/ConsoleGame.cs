using Battleships.IoC;

namespace Battleships
{
    internal class ConsoleGame
    {
        private readonly BoardInitializerFactory _boardInitializerFactory;
        private readonly ConsoleWrapper _consoleWrapper;
        private readonly BoardPrinter _boardPrinter;
        private readonly ConsoleCoordinatesReader _consoleCoordinateReader;
        private readonly CoordinatesMapper _coordinatesMapper;
        private readonly ShotResultMapper _shotResultMapper;

        public ConsoleGame(
            BoardInitializerFactory boardInitializerFactory,
            ConsoleWrapper consoleWrapper,
            BoardPrinter boardPrinter,
            ConsoleCoordinatesReader consoleReader,
            CoordinatesMapper coordinatesMapper,
            ShotResultMapper shotResultMapper)
        {
            _boardInitializerFactory = boardInitializerFactory;
            _consoleWrapper = consoleWrapper;
            _boardPrinter = boardPrinter;
            _consoleCoordinateReader = consoleReader;
            _coordinatesMapper = coordinatesMapper;
            _shotResultMapper = shotResultMapper;
        }

        public void Start()
        {
            var initializer = _boardInitializerFactory.CreateBoardInitializer();
            var board = initializer.Initialize();

            while (!board.IsConquered)
            {
                var inputCoordinates = _consoleCoordinateReader.ReadInput();

                _consoleWrapper.Clear();

                var coordinates = _coordinatesMapper.Map(inputCoordinates);
                var shotResult = _shotResultMapper.Map(board.Check(coordinates));

                _consoleWrapper.Print(_boardPrinter.Print(board));
                _consoleWrapper.Print(shotResult);
            }

            _consoleWrapper.Print("You won! Game Over!");
        }
    }
}
