namespace Battleships.Core
{
    public class Game
    {
        internal Game()
        {
        }

        internal ShotResult Shoot(Board opponent)
        {
            return opponent.Check(null);
        }
    }
}
