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
                game.Start();
            }
        }
    }
}
