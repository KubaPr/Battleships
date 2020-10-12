using System;

namespace Battleships
{
    internal class ConsoleReader
    {
        public virtual string Read()
        {
            return Console.ReadLine();
        }
    }
}
