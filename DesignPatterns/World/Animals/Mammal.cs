using World.Interfaces.Animals;

namespace World.Animals
{
    public abstract class Mammal : Animal, IMammal
    {
        public bool HasFur { get; set; }

        public Mammal(ushort age, sbyte legsCount, sbyte teathCount)
        {
            this.Age = age;
            this.LegsCount = legsCount;
            this.TeathCount = teathCount;
        }
    }
}
