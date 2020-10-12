using System;

namespace Battleships
{
    internal class ConsolePrinter
    {
        public virtual void Print(string stringToPrint)
        {
            Console.WriteLine(stringToPrint);
        }
    }
}