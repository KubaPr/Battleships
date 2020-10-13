using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core.Fleet
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

            foreach (var ship in shipsToBePositioned)
            {
                var shipPositions = GeneratePositions(ship, positions);

                positions.AddRange(shipPositions);
            }

            positions.AddRange(CreateUnocupiedPositions(positions));

            return positions;
        }

        private List<Position> GeneratePositions(Ship ship, List<Position> positions)
        {
            var generatedPositions = _randomPositioner.GeneratePositionsForShip(ship);

            while (AreShipsColliding(generatedPositions, positions))
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

        private IEnumerable<Position> CreateUnocupiedPositions(List<Position> occupiedPositions)
        {
            var unoccupiedPositions = new List<Position>();

            for (var horizontal = 0; horizontal < Board.Size; horizontal++)
            {
                for (var vertical = 0; vertical < Board.Size; vertical++)
                {
                    if (!IsTaken(occupiedPositions, horizontal, vertical))
                    {
                        unoccupiedPositions.Add(new Position(new Coordinates(vertical, horizontal)));
                    }
                }
            }

            return unoccupiedPositions;
        }

        private bool IsTaken(List<Position> occupiedPositions, int horizontal, int vertical)
        {
            return occupiedPositions.Any(position =>
                position.Coordinates.Horizontal == horizontal &&
                position.Coordinates.Vertical == vertical);
        }
    }
}