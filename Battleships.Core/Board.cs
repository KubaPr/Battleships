using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core
{
    internal class Board
    {
        private List<Position> Positions { get; }

        public Board(List<Position> positions)
        {
            Positions = positions;
        }

        public bool IsConquered => Positions.Select(position => position.Occupant).All(occupant => occupant.IsSunk);

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