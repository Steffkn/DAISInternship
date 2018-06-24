namespace NeuralNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Network
    {
        public double LearnRate { get; set; }

        public double Momentum { get; set; }

        public List<Neuron> InputLayer { get; set; }

        public List<List<Neuron>> HiddenLayers { get; set; }

        public List<Neuron> OutputLayer { get; set; }

        private static readonly Random Random = new Random();

        public Network()
        {
            this.LearnRate = 0;
            this.Momentum = 0;
            this.InputLayer = new List<Neuron>();
            this.HiddenLayers = new List<List<Neuron>>();
            this.OutputLayer = new List<Neuron>();
        }

        public Network(int inputSize, int[] hiddenSizes, int outputSize, double learnRate = 0.4, double momentum = 0.9)
        {
            this.LearnRate = learnRate;
            this.Momentum = momentum;
            this.InputLayer = new List<Neuron>();
            this.HiddenLayers = new List<List<Neuron>>();
            this.OutputLayer = new List<Neuron>();

            for (var i = 0; i < inputSize; i++)
            {
                InputLayer.Add(new Neuron());
            }

            var firstHiddenLayer = new List<Neuron>();
            for (var i = 0; i < hiddenSizes[0]; i++)
            {
                firstHiddenLayer.Add(new Neuron(InputLayer));
            }

            HiddenLayers.Add(firstHiddenLayer);

            for (var i = 1; i < hiddenSizes.Length; i++)
            {
                var hiddenLayer = new List<Neuron>();
                for (var j = 0; j < hiddenSizes[i]; j++)
                {
                    hiddenLayer.Add(new Neuron(HiddenLayers[i - 1]));
                }
                HiddenLayers.Add(hiddenLayer);
            }

            for (var i = 0; i < outputSize; i++)
            {
                OutputLayer.Add(new Neuron(HiddenLayers.Last()));
            }
        }

        public void Train(List<DataSet> dataSets, int numEpochs)
        {
            for (var i = 0; i < numEpochs; i++)
            {
                foreach (var dataSet in dataSets)
                {
                    this.ForwardPropagate(dataSet.Values);
                    this.BackPropagate(dataSet.Targets);
                }
            }
        }

        public void Train(List<DataSet> dataSets, double minimumError)
        {
            var error = 1.0;
            var numEpochs = 0;

            while (error > minimumError && numEpochs < int.MaxValue)
            {
                var errors = new List<double>();
                foreach (var dataSet in dataSets)
                {
                    this.ForwardPropagate(dataSet.Values);
                    this.BackPropagate(dataSet.Targets);
                    errors.Add(CalculateError(dataSet.Targets));
                }

                error = errors.Average();
                numEpochs++;
            }
        }

        private void ForwardPropagate(params double[] inputs)
        {
            var i = 0;
            this.InputLayer.ForEach(a => a.Value = inputs[i++]);
            this.HiddenLayers.ForEach(a => a.ForEach(b => b.CalculateValue()));
            this.OutputLayer.ForEach(a => a.CalculateValue());
        }

        private void BackPropagate(params double[] targets)
        {
            var i = 0;

            foreach (var neuron in this.OutputLayer)
            {
                neuron.CalculateGradient(targets[i++]);
            }

            // TODO: change this to be reversed for loop
            this.HiddenLayers.Reverse();

            foreach (var layer in this.HiddenLayers)
            {
                foreach (var neuron in layer)
                {
                    neuron.CalculateGradient();
                }
            }

            foreach (var layer in this.HiddenLayers)
            {
                foreach (var neuron in layer)
                {
                    neuron.UpdateWeights(LearnRate, Momentum);
                }
            }

            this.HiddenLayers.Reverse();

            foreach (var neuron in this.OutputLayer)
            {
                neuron.UpdateWeights(LearnRate, Momentum);
            }
        }

        public double[] Compute(params double[] inputs)
        {
            this.ForwardPropagate(inputs);
            return this.OutputLayer.Select(a => a.Value).ToArray();
        }

        private double CalculateError(params double[] targets)
        {
            var i = 0;
            return this.OutputLayer.Sum(a => Math.Abs(a.CalculateError(targets[i++])));
        }

        public static double GetRandom()
        {
            return 2 * Random.NextDouble() - 1;
        }
    }

    public enum TrainingType
    {
        Epoch,
        MinimumError
    }
}
