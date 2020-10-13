using Battleships.Core;
using Battleships.Core.Fleet;
using System;

namespace Battleships.IoC
{
    public class BoardInitializerFactory
    {
        public virtual BoardInitializer CreateBoardInitializer()
        {
            return new BoardInitializer(
                        new FleetPositioner(
                            new RandomPositioner(
                                new RandomGenerator(new Random()))),
                        new FleetConfigurationProvider());
        }
    }
}
