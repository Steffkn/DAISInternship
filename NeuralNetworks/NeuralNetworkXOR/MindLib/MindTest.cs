namespace MindLib
{
    using MindLib.TransferFunctions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class MindTest
    {
        // we can add seed later
        private static Random Rand = new Random();

        public const double Learning_Rate = 0.8;
        private ITransferFunction function;
        public MindTest()
        {
            this.function = new SigmoidTransferFunction();
            this.Neurons = new List<List<NeuronTest>>();
        }

        /// <summary>
        /// Neurons to hold the information we want
        /// </summary>
        public List<List<NeuronTest>> Neurons { get; set; }

        /// <summary>
        /// list of input rows
        /// </summary>
        public List<List<double>> Inputs { get; set; }

        /// <summary>
        /// list of coresponding outputs rows
        /// </summary>
        public List<List<double>> Outputs { get; set; }

        public void Train(int iterations)
        {
            for (int z = 0; z < iterations; z++)
            {
                foreach (var inputSet in this.Inputs)
                {
                    var inputLayer = this.Neurons[0];
                    for (int i = 0; i < inputSet.Count; i++)
                    {
                        inputLayer[i].NeuronSum.Value = inputSet[i];
                    }

                    ForwardPropagation();

                    BackwardPropagation();
                }
            }
            // MemoryManager.WriteToFile(outputFileName, weights);
        }

        public void ForwardPropagation()
        {
            foreach (var layer in this.Neurons.Skip(1))
            {
                foreach (var neuron in layer)
                {
                    neuron.CalculateValue(this.function.Activate);
                }
            }
        }

        public void BackwardPropagation()
        {
            int lastLayerIndex = this.Neurons.Count - 1;
            var outputLayer = this.Neurons[lastLayerIndex];

            // output layer
            for (int i = 0; i < outputLayer.Count; i++)
            {
                outputLayer[i].DError.Value = this.function.Derivative(outputLayer[i].NeuronSum.Value) * (outputLayer[i].NeuronSum.Value - this.Outputs[i][0]);
            }

            // hidden layers
            for (int layerIndex = lastLayerIndex - 1; layerIndex >= 0; layerIndex--)
            {
                for (int i = 0; i < this.Neurons[layerIndex].Count; i++)
                {
                    double errorCorrection = 0;
                    foreach (var nextLayer in this.Neurons[layerIndex + 1])
                    {
                        errorCorrection += nextLayer.DError.Value * nextLayer.Inputs[i].Weight;
                        nextLayer.Inputs[i].CorrectWeights(Learning_Rate);
                    }

                    this.Neurons[layerIndex][i].DError.Value = this.function.Derivative(this.Neurons[layerIndex][i].NeuronSum.Value) * errorCorrection;
                }
            }
        }

        public void LoadRandomWeights(int inputNeurons, int outputNeurons, int hiddenNeurons, int weightRows, double[][] weights)
        {
            // input weights
            for (int x = 0; x < inputNeurons; x++)
            {
                weights[x] = new double[hiddenNeurons];

                for (int y = 0; y < hiddenNeurons; y++)
                {
                    System.Threading.Thread.Sleep(111);
                    weights[x][y] = Rand.NextDouble();
                }
            }

            // last (1) hidden layer weights
            for (int x = inputNeurons; x < weightRows; x++)
            {
                weights[x] = new double[outputNeurons];

                for (int y = 0; y < outputNeurons; y++)
                {
                    System.Threading.Thread.Sleep(111);
                    weights[x][y] = Rand.NextDouble();
                }
            }
        }

        public void SetUpTrainingData(double[][] inputs, double[][] outputs)
        {
            // set up inputs for training
            this.Inputs = new List<List<double>>();
            for (int i = 0; i < inputs.Length; i++)
            {
                this.Inputs.Add(new List<double>(inputs[i]));
            }

            // set up outputs for training
            this.Outputs = new List<List<double>>();
            for (int i = 0; i < outputs.Length; i++)
            {
                this.Outputs.Add(new List<double>(outputs[i]));
            }
        }

        public void SetUpLayers()
        {
            // TODO: works for rectangular arrays only
            int numberOfInputs = this.Inputs[0].Count;
            int numberOfOutputs = this.Outputs[0].Count;

            int numberOfHiddenLayers = 2;
            int numberOfHiddenNeuronsPerLayers = 3;
            int count = 0;
            this.Neurons = new List<List<NeuronTest>>();
            // set up input layer
            var inputLayer = new List<NeuronTest>();
            for (int i = 0; i < numberOfInputs; i++)
            {
                inputLayer.Add(new NeuronTest(count++));
            }

            this.Neurons.Add(inputLayer);

            // set up the hidden layers
            for (int i = 0; i < numberOfHiddenLayers; i++)
            {
                var newLayer = new List<NeuronTest>();
                for (int y = 0; y < numberOfHiddenNeuronsPerLayers; y++)
                {
                    newLayer.Add(new NeuronTest(count++));
                }

                this.Neurons.Add(newLayer);
            }

            // set up output layer
            var outputLayer = new List<NeuronTest>();

            for (int i = 0; i < numberOfOutputs; i++)
            {
                outputLayer.Add(new NeuronTest(count++));
            }

            this.Neurons.Add(outputLayer);
        }

        public void SetUpSynapses()
        {
            double count = 0.001;
            // for every layer
            for (int i = 1; i < this.Neurons.Count; i++)
            {
                foreach (var currentNeuron in this.Neurons[i])
                {
                    foreach (var prevNeuron in this.Neurons[i - 1])
                    {
                        currentNeuron.Inputs.Add(new Synapse(prevNeuron.NeuronSum, currentNeuron.DError, count));
                        count += 0.001;
                    }
                }
            }
        }

        public List<double> GetResult(List<double> inputSet)
        {
            var inputLayer = this.Neurons[0];
            for (int i = 0; i < inputSet.Count; i++)
            {
                inputLayer[i].NeuronSum.Value = inputSet[i];
            }

            ForwardPropagation();
            return this.Neurons[this.Neurons.Count - 1].Select(neuron => neuron.NeuronSum.Value).ToList();
        }
    }
}
