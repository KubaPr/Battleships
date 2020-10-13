using Battleships.Core;

namespace Battleships.Console
{
    internal class ShotResultMapper
    {
        public virtual string Map(ShotResult shotResult)
        {
            return shotResult.IsHit ? $"You hit a {shotResult.Ship.Name}!" : "Miss!";
        }
    }
}