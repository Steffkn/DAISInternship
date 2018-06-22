namespace NeuralNetwork.Helpers
{
    using System;
    using System.Collections.Generic;

    public class HelperNetwork
    {
        public double LearnRate { get; set; }

        public double Momentum { get; set; }

        public List<HelperNeuron> InputLayer { get; set; }

        public List<List<HelperNeuron>> HiddenLayers { get; set; }

        public List<HelperNeuron> OutputLayer { get; set; }

        public List<HelperSynapse> Synapses { get; set; }

        public HelperNetwork()
        {
            this.InputLayer = new List<HelperNeuron>();
            this.HiddenLayers = new List<List<HelperNeuron>>();
            this.OutputLayer = new List<HelperNeuron>();
            this.Synapses = new List<HelperSynapse>();
        }
    }

    public class HelperNeuron
    {
        public Guid Id { get; set; }

        public double Bias { get; set; }

        public double BiasDelta { get; set; }

        public double Gradient { get; set; }

        public double Value { get; set; }
    }

    public class HelperSynapse
    {
        public Guid Id { get; set; }
        public Guid OutputNeuronId { get; set; }
        public Guid InputNeuronId { get; set; }
        public double Weight { get; set; }
        public double WeightDelta { get; set; }
    }
}
