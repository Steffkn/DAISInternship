namespace CustomNetwork.Interfaces
{
    using System.Collections.Generic;

    public interface ILayer
    {
        LinkedList<INeuron> Neurons { get; }
    }
}
