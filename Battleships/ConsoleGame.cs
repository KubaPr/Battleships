using Battleships.IoC;
using System;

namespace Battleships
{
    internal class ConsoleGame
    {
        private readonly BoardInitializerFactory _boardInitializerFactory;
        private readonly ConsolePrinter _consolePrinter;
        private readonly BoardPrinter _boardPrinter;
        private readonly ConsoleReader _consoleReader;
        private readonly InputMapper _inputMapper;
        private readonly ShotResultMapper _shotResultMapper;

        public ConsoleGame(
            BoardInitializerFactory boardInitializerFactory,
            ConsolePrinter consolePrinter,
            BoardPrinter boardPrinterDouble,
            ConsoleReader consoleReaderDouble,
            InputMapper inputMapperDouble,
            ShotResultMapper shotResultMapper)
        {
            _boardInitializerFactory = boardInitializerFactory;
            _consolePrinter = consolePrinter;
            _boardPrinter = boardPrinterDouble;
            _consoleReader = consoleReaderDouble;
            _inputMapper = inputMapperDouble;
            _shotResultMapper = shotResultMapper;
        }

        public void Start()
        {
            var initializer = _boardInitializerFactory.CreateBoardInitializer();

            var board = initializer.Initialize();

            while (!board.IsConquered)
            {
                _consolePrinter.Print(_boardPrinter.Print(board));

                var input = _consoleReader.ReadInput();

                var coordinates = _inputMapper.Map(input);

                var shotResult = _shotResultMapper.Map(board.Check(coordinates));

                _consolePrinter.Print(shotResult);
            }
        }
    }
}
