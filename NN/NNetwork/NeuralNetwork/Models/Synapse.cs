namespace NeuralNetwork.Models
{
    using System;

    public class Synapse
    {
        public Synapse() { }

        public Synapse(Neuron inputNeuron, Neuron outputNeuron)
        {
            this.Id = Guid.NewGuid();
            this.InputNeuron = inputNeuron;
            this.OutputNeuron = outputNeuron;
            this.Weight = Network.GetRandom();
        }

        public Guid Id { get; set; }

        public Neuron InputNeuron { get; set; }

        public Neuron OutputNeuron { get; set; }

        public double Weight { get; set; }

        public double WeightDelta { get; set; }
    }
}
