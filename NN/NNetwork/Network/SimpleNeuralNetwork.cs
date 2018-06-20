namespace Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Network.ActivationFunctions;
    using Network.Factories;
    using Network.InputFunctions;
    using Network.Layers;

    public class SimpleNeuralNetwork
    {
        private NeuralLayerFactory layerFactory;

        internal List<NeuralLayer> layers;
        internal double learningRate;
        internal double[][] expectedResult;

        /// <summary>
        /// Constructor of the Neural Network.
        /// Note: Initially input layer with defined number of inputs will be created.
        /// </summary>
        /// <param name="numberOfInputNeurons">
        /// Number of neurons in input layer.
        /// </param>
        public SimpleNeuralNetwork(int numberOfInputNeurons)
        {
            layers = new List<NeuralLayer>();
            layerFactory = new NeuralLayerFactory();

            // Create input layer that will collect inputs.
            CreateInputLayer(numberOfInputNeurons);

            learningRate = 2.95;
        }

        /// <summary>
        /// Add layer to the neural network.
        /// Layer will automatically be added as the output layer to the last layer in the neural network.
        /// </summary>
        public void AddLayer(NeuralLayer newLayer)
        {
            if (layers.Any())
            {
                var lastLayer = layers.Last();
                newLayer.ConnectLayers(lastLayer);
            }

            layers.Add(newLayer);
        }

        /// <summary>
        /// Push input values to the neural network.
        /// </summary>
        public void PushInputValues(double[] inputs)
        {
            layers.First().Neurons.ForEach(x => x.PushValueOnInput
                           (inputs[layers.First().Neurons.IndexOf(x)]));
        }

        /// <summary>
        /// Set expected values for the outputs.
        /// </summary>
        public void PushExpectedValues(double[][] expectedOutputs)
        {
            expectedResult = expectedOutputs;
        }

        /// <summary>
        /// Calculate output of the neural network.
        /// </summary>
        /// <returns></returns>
        public List<double> GetOutput()
        {
            var returnValue = new List<double>();

            layers.Last().Neurons.ForEach(neuron =>
            {
                returnValue.Add(neuron.CalculateOutput());
            });

            return returnValue;
        }

        /// <summary>
        /// Train neural network.
        /// </summary>
        /// <param name="inputs">Input values.</param>
        /// <param name="numberOfEpochs">Number of epochs.</param>
        public void Train(double[][] inputs, int numberOfEpochs)
        {
            double totalError = 0;

            for (int i = 0; i < numberOfEpochs; i++)
            {
                for (int j = 0; j < inputs.GetLength(0); j++)
                {
                    PushInputValues(inputs[j]);

                    var outputs = new List<double>();

                    // Get outputs.
                    layers.Last().Neurons.ForEach(x =>
                    {
                        outputs.Add(x.CalculateOutput());
                    });

                    // Calculate error by summing errors on all output neurons.
                    totalError = CalculateTotalError(outputs, j);
                    HandleOutputLayer(j);
                    HandleHiddenLayers();
                }
            }
        }

        /// <summary>
        /// Helper function that creates input layer of the neural network.
        /// </summary>
        private void CreateInputLayer(int numberOfInputNeurons)
        {
            var inputLayer = layerFactory.CreateNeuralLayer(numberOfInputNeurons, new RectifiedActivationFuncion(), new WeightedSumFunction());
            inputLayer.Neurons.ForEach(x => x.AddInputSynapse(0));
            this.AddLayer(inputLayer);
        }

        /// <summary>
        /// Helper function that calculates total error of the neural network.
        /// </summary>
        private double CalculateTotalError(List<double> outputs, int row)
        {
            double totalError = 0;

            outputs.ForEach(output =>
            {
                var error = Math.Pow(output - expectedResult[row][outputs.IndexOf(output)], 2);
                totalError += error;
            });

            return totalError;
        }

        /// <summary>
        /// Helper function that runs backpropagation algorithm on the output layer of the network.
        /// </summary>
        /// <param name="row">
        /// Input/Expected output row.
        /// </param>
        private void HandleOutputLayer(int row)
        {
            layers.Last().Neurons.ForEach(neuron =>
            {
                neuron.Inputs.ForEach(connection =>
                {
                    var output = neuron.CalculateOutput();
                    var netInput = connection.GetOutput();

                    var expectedOutput = expectedResult[row][layers.Last().Neurons.IndexOf(neuron)];

                    var nodeDelta = (expectedOutput - output) * output * (1 - output);
                    var delta = -1 * netInput * nodeDelta;

                    connection.UpdateWeight(learningRate, delta);

                    neuron.PreviousPartialDerivate = nodeDelta;
                });
            });
        }

        /// <summary>
        /// Helper function that runs backpropagation algorithm on the hidden layer of the network.
        /// </summary>
        /// <param name="row">
        /// Input/Expected output row.
        /// </param>
        private void HandleHiddenLayers()
        {
            for (int k = layers.Count - 2; k > 0; k--)
            {
                layers[k].Neurons.ForEach(neuron =>
                {
                    neuron.Inputs.ForEach(connection =>
                    {
                        var output = neuron.CalculateOutput();
                        var netInput = connection.GetOutput();
                        double sumPartial = 0;

                        layers[k + 1].Neurons
                        .ForEach(outputNeuron =>
                        {
                            outputNeuron.Inputs.Where(i => i.IsFromNeuron(neuron.Id))
                            .ToList()
                            .ForEach(outConnection =>
                            {
                                sumPartial += outConnection.PreviousWeight *
                                                    outputNeuron.PreviousPartialDerivate;
                            });
                        });

                        var delta = -1 * netInput * sumPartial * output * (1 - output);
                        connection.UpdateWeight(learningRate, delta);
                    });
                });
            }
        }
    }
}
