namespace Battleships.Core
{
    public class ShotResult
    {
        public bool IsHit => Ship != null;
        public Ship Ship { get; }

        public ShotResult()
        {
        }

        public ShotResult(Ship ship)
        {
            Ship = ship;
        }
    }
}