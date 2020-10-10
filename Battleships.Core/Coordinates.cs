namespace Battleships.Core
{
    internal class Coordinates
    {
        public int Vertical { get; }
        public int Horizontal { get; }

        public Coordinates(int vertical, int horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }
    }
}