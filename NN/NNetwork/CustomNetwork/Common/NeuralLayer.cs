namespace CustomNetwork
{
    using CustomNetwork.Interfaces;
    using System;
    using System.Collections.Generic;

    public class NeuralLayer : ILayer
    {
        private int numberOfNeurons;

        // SigmoidActivationFunction coeficient
        private const double coeficient = 0.7;

        public NeuralLayer(int numberOfNeurons = 10)
        {
            this.numberOfNeurons = numberOfNeurons;
            this.Neurons = new LinkedList<INeuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                this.Neurons.AddLast(new Neuron());
            }
        }

        public LinkedList<INeuron> Neurons { get; }

        public double ActivationFunction(double input)
        {
            return (1 / (1 + Math.Exp(-input * coeficient)));
        }
    }
}
