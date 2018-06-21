namespace CustomNetwork.Interfaces
{
    using CustomNetwork.Common;

    public interface ISynapse
    {
        Content ConnectedNeuronContent { get; }

        double Weight { get; }

        double PreviousWeight { get; }

        void UpdateWeight(double learningRate, double delta);
    }
}
