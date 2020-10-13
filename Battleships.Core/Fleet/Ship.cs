using System;

namespace Battleships.Core.Fleet
{
    public abstract class Ship
    {
        public abstract string Name { get; }
        internal int NumberOfMasts { get; }
        internal bool IsSunk => _hitMastsCount == NumberOfMasts;
        internal virtual void ReportHit() => _hitMastsCount++;

        private int _hitMastsCount;

        public Ship(int numberOfMasts)
        {
            if (numberOfMasts == 0) throw new ArgumentException("Ship must have at least one mast.");

            NumberOfMasts = numberOfMasts;
            _hitMastsCount = 0;
        }
    }
}