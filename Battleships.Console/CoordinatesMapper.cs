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
            return input switch
            {
                'A' => 0,
                'B' => 1,
                'C' => 2,
                'D' => 3,
                'E' => 4,
                'F' => 5,
                'G' => 6,
                'H' => 7,
                'I' => 8,
                'J' => 9,
                _ => throw new ArgumentException(),
            };
        }
    }
}