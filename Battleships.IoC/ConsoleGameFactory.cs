using Battleships.Console;

namespace Battleships.IoC
{
    public class ConsoleGameFactory
    {
        public ConsoleGame CreateConsoleGame()
        {
            return new ConsoleGame(
                new BoardInitializerFactory().CreateBoardInitializer(),
                new ConsoleWrapper(),
                new BoardPrinter(
                    new PositionStateMapper()),
                new ConsoleCoordinatesReader(
                    new ConsoleWrapper()),
                new CoordinatesMapper(),
                new ShotResultMapper());
        }
    }
}
