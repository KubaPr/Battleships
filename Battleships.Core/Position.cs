using System;

namespace Battleships.Core
{
    internal class Position
    {
        public Coordinates Coordinates { get; }
        public bool IsOccupied => Occupant != null;
        public Ship Occupant { get; }
        public PositionState State { get; private set; }

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