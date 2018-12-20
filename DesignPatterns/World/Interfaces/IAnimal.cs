namespace World.Interfaces
{
    public interface IAnimal
    {
        ushort Age { get; }

        sbyte LegsCount { get; set; }

        sbyte TeathCount { get; set; }

        void Move();

        void Breathе();
    }
}
