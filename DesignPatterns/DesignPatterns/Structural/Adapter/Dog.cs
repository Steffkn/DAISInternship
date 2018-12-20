using System;
using World.Interfaces;

namespace DesignPatterns.Structural.Adapter
{
    public class Dog : IMakeSound
    {
        public void MakeSound()
        {
            Console.WriteLine("Barking..");
        }
    }
}
