using System;

namespace Battleships.Core
{
    internal class RandomGenerator
    {
        private readonly Random _random;

        public RandomGenerator(Random random)
        {
            _random = random;
        }

        public virtual bool GenerateRandomBool()
        {
            return _random.NextDouble() > 0.5;
        }

        public virtual int GenerateRandomNumber(int from, int to)
        {
            return _random.Next(from, to);
        }
    }
}