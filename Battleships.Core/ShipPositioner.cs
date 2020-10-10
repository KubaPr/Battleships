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

        public virtual List<Position> CreatePositions()
        {
            //TODO: to rethink. This should probably be passed to this class and created elswhere.
            var shipsToBePositioned = new List<Ship>
            {
                new Ship(5),
                new Ship(4),
                new Ship(4)
            };

            var positions = new List<Position>();

            foreach(var ship in shipsToBePositioned)
            {
                var shipPositions = GeneratePositions(ship, positions);

                positions.AddRange(shipPositions);
            }

            return positions;
        }

        private List<Position> GeneratePositions(Ship ship, List<Position> positions)
        {
            var generatedPositions = _randomPositioner.GeneratePositionsForShip(ship);

            while(AreShipsColliding(generatedPositions, positions))
            {
                generatedPositions.Clear();
                generatedPositions = _randomPositioner.GeneratePositionsForShip(ship);
            }

            return generatedPositions;
        }

        private bool AreShipsColliding(List<Position> generatedPositions, List<Position> positions)
        {
            return generatedPositions.Any(position => AreTooClose(position.Coordinates, positions));
        }

        private bool AreTooClose(Coordinates coordinates, List<Position> alreadyTakenPositions)
        {
            const int minimumOffset = 1;

            return alreadyTakenPositions.Any(takenPosition =>
                Math.Abs(takenPosition.Coordinates.Horizontal - coordinates.Horizontal) <= minimumOffset &&
                Math.Abs(takenPosition.Coordinates.Vertical - coordinates.Vertical) <= minimumOffset);
        }
    }
}