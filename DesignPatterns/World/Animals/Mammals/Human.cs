using System;
using World.Interfaces.Animals.Mammals;

namespace World.Animals.Mammals
{
    public class Human : Mammal, IHuman
    {
        public Human(ushort age)
            : base(age, 2, 32)
        {
        }

        public override void Breathе()
        {
            Console.WriteLine("aaaAAA ... HHHhhhhhhhh...");
        }

        public override void Move()
        {
            Console.WriteLine("Po-pri-mryd");
        }
    }
}
