using SysConsole = System.Console;

namespace Battleships.Console
{
    internal class ConsoleWrapper
    {
        public virtual void Print(string stringToPrint)
        {
            SysConsole.WriteLine(stringToPrint);
        }

        public virtual string Read()
        {
            return SysConsole.ReadLine();
        }

        public virtual void Clear()
        {
            SysConsole.Clear();
        }
    }
}