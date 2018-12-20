using System.Collections.Generic;
using World.Interfaces;

namespace DesignPatterns.Structural.Adapter
{
    public class AdapterTest : IRunable
    {
        public void Run()
        {
            List<IMakeSound> soundMakers = new List<IMakeSound>();
            soundMakers.Add(new Dog());
            soundMakers.Add(new DuckAdapter(new Duck()));

            foreach (var animal in soundMakers)
            {
                animal.MakeSound();
            }
        }
    }
}
