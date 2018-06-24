namespace CustomNetwork
{
    using System;
    using System.Collections.Generic;
    using CustomNetwork.Common;
    using CustomNetwork.Interfaces;

    public class Neuron : INeuron
    {
        public Neuron()
        {
            this.Id = Guid.NewGuid();
            this.Inputs = new List<ISynapse>();
            this.Content = new Content();
        }

        public Guid Id { get; private set; }

        public List<ISynapse> Inputs { get; private set; }

        public Content Content { get; }

        public double PreviousPartialDerivate { get; }

        public double CalculateValue(Func<double, double> activationFunction)
        {
            double inputSum = 0;
            foreach (var synapse in this.Inputs)
            {
                inputSum += synapse.Weight * synapse.ConnectedNeuronContent.Value;
            }

            this.Content.Value = activationFunction.Invoke(inputSum);
            return this.Content.Value;
        }
    }
}
