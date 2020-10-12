using System;
using System.Text.RegularExpressions;

namespace Battleships
{
    internal class ConsoleCoordinatesReader
    {
        private readonly ConsolePrinter _consolePrinter;
        private readonly ConsoleReader _consoleReader;
        private readonly Regex _regex;

        public ConsoleCoordinatesReader(ConsolePrinter consolePrinter, ConsoleReader consoleReader)
        {
            _consolePrinter = consolePrinter;
            _consoleReader = consoleReader;

             _regex = new Regex(@"^[a-j][0-9]{1}$", RegexOptions.IgnoreCase);
        }

        public virtual string ReadInput()
        {
            var input = _consoleReader.Read();

            if (!IsValid(input))
            {
                _consolePrinter.Print("Invalid shot. Shoot between A0 and J9");

                return ReadInput();
            };

            return input.ToUpper();
        }

        private bool IsValid(string input) => _regex.IsMatch(input);
    }
}