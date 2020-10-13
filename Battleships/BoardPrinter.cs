using Battleships.Core;
using System;
using System.Linq;
using System.Text;

namespace Battleships
{
    internal class BoardPrinter
    {
        private readonly PositionStateMapper _positionStateMapper;

        public BoardPrinter(PositionStateMapper positionStateMapper)
        {
            _positionStateMapper = positionStateMapper;
        }

        public virtual string Print(Board board)
        {
            //TODO: refactor
            var line = new StringBuilder();
            line.Append("  A B C D E F G H I J");

            for (var i = 0; i < Board.Size; i++)
            {
                var row = new StringBuilder();
                row.Append(i + " ");
                for (var y = 0; y < Board.Size; y++)
                {
                    var state = board.Positions.SingleOrDefault(
                        pos => pos.Coordinates.Horizontal == i && pos.Coordinates.Vertical == y).State;
                    row.Append(_positionStateMapper.Map(state) + " ");
                }

                line.Append(Environment.NewLine + row);
            }

            return line.ToString();
        }
    }
}