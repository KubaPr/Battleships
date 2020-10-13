namespace Battleships.Core.Fleet
{
    public class Battleship : Ship
    {
        private const int battleshipMastsCount = 5;

        public Battleship() : base(battleshipMastsCount)
        {
        }

        public override string Name => "Battleship";
    }
}
