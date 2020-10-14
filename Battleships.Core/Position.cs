using Battleships.Core.Fleet;
using System;

namespace Battleships.Core
{
    public class Position
    {
        public Coordinates Coordinates { get; }
        public PositionState State { get; private set; }
        internal bool IsOccupied => Occupant != null;
        internal Ship Occupant { get; }

        public Position(Coordinates coordinates, Ship occupant = null)
        {
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
            Occupant = occupant;
            State = PositionState.Unchecked;
        }

        public virtual void MarkAsChecked()
        {
            if (!IsOccupied)
            {
                State = PositionState.Miss;
                return;
            }

            if (State == PositionState.Unchecked)
            {
                Occupant.ReportHit();
            }

            State = PositionState.Hit;
        }
    }
}