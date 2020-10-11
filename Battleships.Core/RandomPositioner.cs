using System;
using System.Collections.Generic;

namespace Battleships.Core
{
    internal class RandomPositioner
    {
        private readonly int _boardSize;
        private readonly RandomGenerator _randomGenerator;

        public RandomPositioner(int boardSize, RandomGenerator randomGenerator)
        {
            _boardSize = boardSize;
            _randomGenerator = randomGenerator;
        }

        public virtual List<Position> GeneratePositionsForShip(Ship ship)
        {
            if (ship.NumberOfMasts > _boardSize)
            {
                throw new ArgumentException("Ship cannot be bigger than the board");
            }

            var isHorizontallyOriented = _randomGenerator.GenerateRandomBool();
            var boardFittingInitialCoordinate = _randomGenerator.GenerateRandomNumber(0, _boardSize - 1);
            var shipFittingInitialCoordinate = _randomGenerator.GenerateRandomNumber(0, _boardSize - 1 - ship.NumberOfMasts);

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