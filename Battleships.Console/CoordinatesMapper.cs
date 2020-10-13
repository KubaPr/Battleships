using Battleships.Core;
using System;
using System.Linq;

namespace Battleships.Console
{
    internal class CoordinatesMapper
    {
        public virtual Coordinates Map(string stringCoordinates)
        {
            return new Coordinates
                (Map(stringCoordinates.First()),
                int.Parse(stringCoordinates.Skip(1).First().ToString()));
        }

        private static int Map(char input)
        {
            switch (input)
            {
                case 'A': return 0;
                case 'B': return 1;
                case 'C': return 2;
                case 'D': return 3;
                case 'E': return 4;
                case 'F': return 5;
                case 'G': return 6;
                case 'H': return 7;
                case 'I': return 8;
                case 'J': return 9;
                default: throw new ArgumentException();
            }
        }
    }
}