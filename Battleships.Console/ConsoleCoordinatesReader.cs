using System.Text.RegularExpressions;

namespace Battleships.Console
{
    internal class ConsoleCoordinatesReader
    {
        private readonly ConsoleWrapper _consoleWrapper;
        private readonly Regex _regex;

        public ConsoleCoordinatesReader(ConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;

            _regex = new Regex(@"^[a-j][0-9]{1}$", RegexOptions.IgnoreCase);
        }

        public virtual string ReadInput()
        {
            var input = _consoleWrapper.Read();

            if (!IsValid(input))
            {
                _consoleWrapper.Print("Invalid shot. Shoot between A0 and J9");

                return ReadInput();
            };

            return input.ToUpper();
        }

        private bool IsValid(string input) => _regex.IsMatch(input);
    }
}