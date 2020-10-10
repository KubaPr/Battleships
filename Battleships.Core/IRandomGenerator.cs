namespace Battleships.Core
{
    public interface IRandomGenerator
    {
        bool GenerateRandomBool();
        int GenerateRandomNumber(int from, int to);
    }
}