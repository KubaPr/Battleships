using System;

namespace Battleships.Core
{
    public class Ship
    {
        public byte NumberOfMasts { get; }
        internal bool IsSunk => _hitMastsCount == NumberOfMasts;
        internal virtual void ReportHit() => _hitMastsCount++;

        private byte _hitMastsCount;

        public Ship(byte numberOfMasts)
        {
            if (numberOfMasts == 0) throw new ArgumentException("Ship must have at least one mast.");

            NumberOfMasts = numberOfMasts;
            _hitMastsCount = 0;
        }
    }
}