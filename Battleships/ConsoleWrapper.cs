using System;

namespace Battleships
{
    internal class ConsoleWrapper
    {
        public virtual void Print(string stringToPrint)
        {
            Console.WriteLine(stringToPrint);
        }

        public virtual string Read()
        {
            return Console.ReadLine();
        }

        public virtual void Clear()
        {
            Console.Clear();
        }
    }
}