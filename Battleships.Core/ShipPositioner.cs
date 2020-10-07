using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core
{
    internal class ShipPositioner
    {
        private readonly RandomPositioner _randomPositioner;

        public ShipPositioner(RandomPositioner randomPositioner)
        {
            _randomPositioner = randomPositioner;
        }

        public virtual List<Position> GetPositions()
        {
            var positions = new List<Position>();

            var shipsToBePositioned = new List<Ship>
            {
                new Ship(5),
                new Ship(4),
                new Ship(4)
            };

            foreach(var ship in shipsToBePositioned)
            {
                var shipPositions = GeneratePositions(ship, positions);

                positions.AddRange(shipPositions);
            }

            return positions;
        }

        private List<Position> GeneratePositions(Ship ship, List<Position> positions)
        {
            var generatedPositions = _randomPositioner.CreatePositionsForShip(ship);

            while(IsAnyPositionInvalid(generatedPositions, positions))
            {
                generatedPositions.Clear();
                generatedPositions = _randomPositioner.CreatePositionsForShip(ship);
            }

            return generatedPositions;
        }

        private bool IsAnyPositionInvalid(List<Position> generatedPositions, List<Position> positions)
        {
            return generatedPositions.Any(position => IsTooClose(position.Coordinates, positions));
        }

        private bool IsTooClose(Coordinates coordinates, List<Position> alreadyTakenPositions)
        {
            const byte minimumOffset = 1;

            return alreadyTakenPositions.Any(takenPosition =>
                Math.Abs(takenPosition.Coordinates.Horizontal - coordinates.Horizontal) < minimumOffset ||
                Math.Abs(takenPosition.Coordinates.Vertical - coordinates.Vertical) < minimumOffset);
        }
    }
}