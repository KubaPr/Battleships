using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core
{
    internal class FleetPositioner
    {
        private readonly RandomPositioner _randomPositioner;

        public FleetPositioner(RandomPositioner randomPositioner)
        {
            _randomPositioner = randomPositioner;
        }

        public virtual List<Position> CreatePositions(List<Ship> shipsToBePositioned)
        {
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