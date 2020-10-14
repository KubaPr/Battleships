using Battleships.IoC;

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
