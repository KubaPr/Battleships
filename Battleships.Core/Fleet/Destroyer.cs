﻿namespace Battleships.Core.Fleet
{
    public class Destroyer : Ship
    {
        private const int destroyerMastsCount = 4;

        public Destroyer() : base(destroyerMastsCount)
        {
        }

        public override string Name => "Destroyer";
    }
}

