using Battleships.IoC;

namespace Battleships
{
    internal class ConsoleGame
    {
        private readonly BoardInitializerFactory _boardInitializerFactory;
        private readonly ConsolePrinter _consolePrinter;
        private readonly BoardPrinter _boardPrinter;
        private readonly ConsoleCoordinatesReader _consoleCoordinateReader;
        private readonly CoordinatesMapper _coordinatesMapper;
        private readonly ShotResultMapper _shotResultMapper;

        public ConsoleGame(
            BoardInitializerFactory boardInitializerFactory,
            ConsolePrinter consolePrinter,
            BoardPrinter boardPrinter,
            ConsoleCoordinatesReader consoleReader,
            CoordinatesMapper coordinatesMapper,
            ShotResultMapper shotResultMapper)
        {
            _boardInitializerFactory = boardInitializerFactory;
            _consolePrinter = consolePrinter;
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
                _consolePrinter.Print(_boardPrinter.Print(board));

                var inputCoordinates = _consoleCoordinateReader.ReadInput();
                var coordinates = _coordinatesMapper.Map(inputCoordinates);
                var shotResult = _shotResultMapper.Map(board.Check(coordinates));

                //TODO: clear console
                _consolePrinter.Print(shotResult);
            }
        }
    }
}
