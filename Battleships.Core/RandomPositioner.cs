using System;
using System.Collections.Generic;

namespace Battleships.Core
{
    internal class RandomPositioner
    {
        private readonly RandomGenerator _randomGenerator;

        public RandomPositioner(RandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public virtual List<Position> GeneratePositionsForShip(Ship ship)
        {
            if (ship.NumberOfMasts > Board.Size)
            {
                throw new ArgumentException("Ship cannot be bigger than the board");
            }

            var isHorizontallyOriented = _randomGenerator.GenerateRandomBool();
            var boardFittingInitialCoordinate = _randomGenerator.GenerateRandomNumber(0, Board.Size - 1);
            var shipFittingInitialCoordinate = _randomGenerator.GenerateRandomNumber(0, Board.Size - 1 - ship.NumberOfMasts);

            var positions = new List<Position>();

            for (var i = shipFittingInitialCoordinate; i < ship.NumberOfMasts + shipFittingInitialCoordinate; i++)
            {
                positions.Add(
                    isHorizontallyOriented ?
                    new Position(new Coordinates(boardFittingInitialCoordinate, i), ship) :
                    new Position(new Coordinates(i, boardFittingInitialCoordinate), ship));
            }

            return positions;
        }
    }
}