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
                new ConsoleWrapper(),
                new BoardPrinter(
                    new PositionStateMapper()),
                new ConsoleCoordinatesReader(
                    new ConsoleWrapper()),
                new CoordinatesMapper(),
                new ShotResultMapper());

            game.Start();
        }
    }
}
