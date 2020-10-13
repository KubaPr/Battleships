using Battleships.IoC;

using SystemConsole = System.Console;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            var game = new ConsoleGameFactory().CreateConsoleGame();

            while (true)
            {
                SystemConsole.WriteLine("This is a one sided Battleships game! Shot bettween A0 and J9 to start a game!");
                game.Start();
                SystemConsole.WriteLine("Press any key to play again");
                SystemConsole.ReadKey();
                SystemConsole.Clear();
            }
        }
    }
}
