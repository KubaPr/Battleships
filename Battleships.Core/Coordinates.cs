namespace Battleships.Core
{
    internal class Coordinates
    {
        public byte Vertical { get; }
        public byte Horizontal { get; }

        public Coordinates(byte vertical, byte horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }
    }
}