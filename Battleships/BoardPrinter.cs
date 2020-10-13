using Battleships.Core;
using System;
using System.Linq;

namespace Battleships
{
    internal class BoardPrinter
    {
        private const string space = " ";

        private readonly PositionStateMapper _positionStateMapper;

        public BoardPrinter(PositionStateMapper positionStateMapper)
        {
            _positionStateMapper = positionStateMapper;
        }

        public virtual string Print(Board board)
        {
            const string header = "  A B C D E F G H I J";

            var boardStringRepresentation =
                string.Join(Environment.NewLine, board.Positions
                    .GroupBy(pos => pos.Coordinates.Horizontal)
                    .OrderBy(pos => pos.Key)
                    .Select(group => PrintStatesForRow(board, group.Key)));

            return $"{header}{Environment.NewLine}{boardStringRepresentation}";
        }

        private string PrintStatesForRow(Board board, int rowNumber)
        {
            var row = string.Join(space, board.Positions
                .Where(pos => pos.Coordinates.Horizontal == rowNumber)
                .OrderBy(pos => pos.Coordinates.Vertical)
                .Select(pos => _positionStateMapper.Map(pos.State)));

            return $"{rowNumber}{space}{row}";
        }
    }
}