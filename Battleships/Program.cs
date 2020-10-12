using System;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello Battleships!");

            var game = new ConsoleGame(null, null, null, null, null, null);

            game.Start();
        }
    }
}
