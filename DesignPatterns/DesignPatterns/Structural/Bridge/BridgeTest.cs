using System;
using System.Collections.Generic;
using System.Threading;

namespace DesignPatterns.Structural.Bridge
{
    public class BridgeTest : IRunable
    {
        public void Run()
        {
            List<Counter> universeCounters = new List<Counter>();

            universeCounters.Add(new AlienCounter());
            universeCounters.Add(new EarthCounter());

            while (true)
            {
                Console.Clear();
                foreach (var counter in universeCounters)
                {
                    counter.Tick();
                }
                foreach (var counter in universeCounters)
                {
                    Console.WriteLine(counter.ToString());
                }

                Thread.Sleep(1000);
            }
        }
    }
}
