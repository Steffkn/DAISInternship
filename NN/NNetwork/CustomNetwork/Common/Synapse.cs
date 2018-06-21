namespace CustomNetwork
{
    using CustomNetwork.Common;
    using CustomNetwork.Interfaces;

    public class Synapse : ISynapse
    {
        public Synapse(INeuron neuron)
        {
            this.ConnectedNeuronContent = neuron.Content;
        }

        public Content ConnectedNeuronContent { get; }

        public double Weight { get; private set; }

        public double PreviousWeight { get; private set; }

        public void UpdateWeight(double learningRate, double delta)
        {
            this.PreviousWeight = Weight;
            this.Weight += learningRate * delta;
        }
    }
}
