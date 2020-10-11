using Battleships.Core;
using System;

namespace Battleships.IoC
{
    public class BoardInitializerFactory
    {
        public static BoardInitializer CreateBoardInitializer()
        {
            return new BoardInitializer(
                        new FleetPositioner(
                            new RandomPositioner(
                                new RandomGenerator(new Random()))),
                        new FleetConfigurationProvider());
        }
    }
}
