using System;
using System.Collections.Generic;
using System.Text;
using World.Interfaces;

namespace DesignPatterns.Structural.Adapter
{
    public class DuckAdapter : IMakeSound
    {
        private readonly Duck duck;

        public DuckAdapter(Duck duck)
        {
            this.duck = duck;
        }

        public void MakeSound()
        {
            this.duck.Quak();
        }
    }
}
