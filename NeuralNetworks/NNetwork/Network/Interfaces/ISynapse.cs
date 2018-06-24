namespace Network.Interfaces
{
    using System;

    public interface ISynapse
    {
        double Weight { get; }

        double Output { get; }

        double PreviousWeight { get; }

        bool IsFromNeuron(Guid fromNeuronId);

        void UpdateWeight(double learningRate, double delta);
    }
}
