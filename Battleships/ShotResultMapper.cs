using Battleships.Core;

namespace Battleships
{
    internal class ShotResultMapper
    {
        public virtual string Map(ShotResult shotResult)
        {
            return shotResult.IsHit ? $"You hit {shotResult.Ship.NumberOfMasts}-masted ship!" : "Miss!";
        }
    }
}