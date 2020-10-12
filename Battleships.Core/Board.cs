using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core
{
    public class Board
    {
        public List<Position> Positions { get; }

        public static readonly int Size = 10;

        internal Board(List<Position> positions)
        {
            Positions = positions;
        }

        public virtual bool IsConquered => Positions
            .Select(position => position.Occupant)
            .Where(occupant => occupant != null)
            .All(occupant => occupant.IsSunk);

        public virtual ShotResult Check(Coordinates coordinates)
        {
            var position = GetPosition(coordinates);
            position.MarkAsChecked();

            if (position.IsOccupied)
            {
                return new ShotResult(position.Occupant);
            }

            return new ShotResult();
        }

        private Position GetPosition(Coordinates coordinates)
        {
            return Positions.Single(position =>
                position.Coordinates.Horizontal == coordinates.Horizontal &&
                position.Coordinates.Vertical == coordinates.Vertical);
        }
    }
}