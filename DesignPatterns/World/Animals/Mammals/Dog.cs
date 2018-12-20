using System;
using System.Collections.Generic;
using System.Text;

namespace World.Animals.Mammals
{
    public class Dog : Mammal
    {
        public Dog(ushort age)
            : base(age, 4, 42)
        {
        }

        public override void Breathе()
        {
            Console.WriteLine("Bark..");
        }

        public override void Move()
        {
            Console.WriteLine("Chasing tail..");
        }
    }
}
