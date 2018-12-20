namespace World.Interfaces.Animals
{
    public interface IMammal : IAnimal
    {
        bool HasFur { get; set; }
    }
}
