using System;

namespace World.Animals.Mammals
{
    public class Mouse : Mammal
    {
        public Mouse(ushort age) 
            : base(age, 4, 18)
        {
        }

        public override void Breathе()
        {
            Console.WriteLine("Cyr..");
        }

        public override void Move()
        {
            Console.WriteLine("Sneaking..");
        }
    }
}
