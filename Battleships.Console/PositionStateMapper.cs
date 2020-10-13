using Battleships.Core;
using System;

namespace Battleships.Console
{
    internal class PositionStateMapper
    {
        public virtual string Map(PositionState state)
        {
            switch (state)
            {
                case PositionState.Hit:
                    return "X";
                case PositionState.Miss:
                    return "O";
                case PositionState.Unchecked:
                    return "~"; ;
                default:
                    throw new ArgumentException();
            }
        }
    }
}