using Battleships.Core;
using Battleships.Core.Fleet;
using System;

namespace Battleships.IoC
{
    internal class BoardInitializerFactory
    {
        public BoardInitializer CreateBoardInitializer()
        {
            return new BoardInitializer(
                        new FleetPositioner(
                            new RandomPositioner(
                                new RandomGenerator(new Random()))),
                        new FleetConfigurationProvider());
        }
    }
}
