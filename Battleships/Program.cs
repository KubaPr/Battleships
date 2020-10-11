using System;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello Battleships!");

            var game = new ConsoleGame();

            game.Start();
        }
    }
}
