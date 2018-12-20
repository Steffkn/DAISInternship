using System;

namespace DesignPatterns.Structural.Bridge
{
    public class AlienCounter : Counter
    {
        public override void Tick()
        {
            // bridge implementation
            this.Count += DateTime.Now.Day;
        }
    }
}
