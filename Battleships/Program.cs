using System;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello Battleships!");

            var game = new ConsoleGame(
                new IoC.BoardInitializerFactory(),
                new ConsolePrinter(),
                new BoardPrinter(
                    new PositionStateMapper()),
                new ConsoleCoordinatesReader(
                    new ConsolePrinter(),
                    new ConsoleReader()),
                new CoordinatesMapper(),
                new ShotResultMapper());

            game.Start();
        }
    }
}
