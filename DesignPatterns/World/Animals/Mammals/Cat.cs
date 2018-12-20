using System;

namespace World.Animals.Mammals
{
    public class Cat : Mammal
    {
        public Cat(ushort age) 
            : base(age, 4, 30)
        {
        }

        public override void Breathе()
        {
            Console.WriteLine("Purring..");
        }

        public override void Move()
        {
            Console.WriteLine("Sneaking..");
        }
    }
}
