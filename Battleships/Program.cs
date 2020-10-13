using System;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            var game = CreateGame();

            while (true)
            {
                Console.WriteLine("This is a one sided Battleships game! Shot bettween A0 and J9 to start a game!");
                game.Start();
                Console.WriteLine("Press any key to play again");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static ConsoleGame CreateGame()
        {
            return new ConsoleGame(
                new IoC.BoardInitializerFactory().CreateBoardInitializer(),
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
