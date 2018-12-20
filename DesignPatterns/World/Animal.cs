using World.Interfaces;

namespace World
{
    public abstract class Animal : IAnimal
    {
        public ushort Age { get; set; }

        public sbyte LegsCount { get; set; }

        public sbyte TeathCount { get; set; }

        public abstract void Move();

        public abstract void Breathе();
    }
}
