using System;

namespace SickOfNN
{
    public class Launcher
    {
        private const int NumberOfHiddenLayers = 2;
        private const int NumberOfHiddenNeurons = 3;
        private const int Epoches = 100000;
        private const double LearningRage = 0.1;
        private static Random rand = new Random();

        static void Main()
        {
            // add bias
            int[,] testDataInputs = {
                { 0, 0 },
                { 0, 1 },
                { 1, 0 },
                { 1, 1 }
            };

            int[,] testDataOutputs = {
                { 0 },
                { 0 },
                { 0 },
                { 1 }
            };

            int numberOfInputs = testDataInputs.GetLength(1);
            int numberOfOutputs = testDataOutputs.GetLength(1);
            int[] hiddenLayersNeuronCounts = new int[NumberOfHiddenLayers] { NumberOfHiddenNeurons, NumberOfHiddenNeurons };

            // input layer
            double[] inputNeurons = new double[numberOfInputs];

            // Hidden layers
            double[][] neuronNetValue = SetUpNeurons(hiddenLayersNeuronCounts);
            double[][] neuronValue = SetUpNeurons(hiddenLayersNeuronCounts);
            double[][] neuronErrors = SetUpNeurons(hiddenLayersNeuronCounts);
            double[][] neuronErrorDeltas = SetUpNeurons(hiddenLayersNeuronCounts);

            // output layer
            double[] outputNeuronsNetValue = new double[numberOfOutputs];
            double[] outputNeuronsValue = new double[numberOfOutputs];
            double[] outputError = new double[numberOfOutputs];
            double[] outputErrorDelta = new double[numberOfOutputs];

            // weights and deltaWeigths
            double[][][] weights = SetUpWeigths(numberOfInputs, numberOfOutputs, hiddenLayersNeuronCounts);
            double[][][] deltaWeights = SetUpDeltaWeights(weights);

            // training
            for (int z = 0; z <= Epoches; z++)
            {
                var input = new int[numberOfInputs];

                for (int dataRow = 0; dataRow < testDataInputs.GetLength(0); dataRow++)
                {
                    // read input

                    if (z == Epoches)
                    {
                        for (int i = 0; i < numberOfInputs; i++)
                        {
                            Console.Write("Enter value {0}: ", i);
                            inputNeurons[i] = double.Parse(Console.ReadLine());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < numberOfInputs; i++)
                        {
                            inputNeurons[i] = testDataInputs[dataRow, i];
                        }
                    }

                    //read output

                    //// forward propagation
                    // calculate first neural layer
                    for (int neuronIndex = 0; neuronIndex < neuronNetValue[0].Length; neuronIndex++)
                    {
                        // todo: replace sum with neuronNetValue[0][neuronIndex]
                        double sum = 0;
                        for (int inputIndex = 0; inputIndex < inputNeurons.Length; inputIndex++)
                        {
                            sum += inputNeurons[inputIndex] * weights[0][neuronIndex][inputIndex];
                        }

                        neuronNetValue[0][neuronIndex] = sum;
                        neuronValue[0][neuronIndex] = Sigmoid(neuronNetValue[0][neuronIndex]);
                    }

                    // calculate hidden layers
                    for (int layerIndex = 1; layerIndex < NumberOfHiddenLayers; layerIndex++)
                    {
                        for (int currentNeuron = 0; currentNeuron < neuronNetValue[layerIndex].Length; currentNeuron++)
                        {
                            double sum = 0;
                            for (int prevNeuronIndex = 0; prevNeuronIndex < neuronNetValue[layerIndex - 1].Length; prevNeuronIndex++)
                            {
                                // TODO: fishy
                                sum += neuronNetValue[layerIndex - 1][prevNeuronIndex] * weights[layerIndex][currentNeuron][prevNeuronIndex];
                            }

                            neuronNetValue[layerIndex][currentNeuron] = sum;
                            neuronValue[layerIndex][currentNeuron] = Sigmoid(neuronNetValue[layerIndex][currentNeuron]);
                        }
                    }

                    // calculate output layer
                    for (int outputNeuronIndex = 0; outputNeuronIndex < outputNeuronsNetValue.Length; outputNeuronIndex++)
                    {
                        double sum = 0;

                        // last hidden layer
                        for (int prevNeuronIndex = 0; prevNeuronIndex < neuronNetValue[NumberOfHiddenLayers - 1].Length; prevNeuronIndex++)
                        {
                            sum += neuronNetValue[NumberOfHiddenLayers - 1][prevNeuronIndex] * weights[NumberOfHiddenLayers - 1][outputNeuronIndex][prevNeuronIndex];
                        }

                        outputNeuronsNetValue[outputNeuronIndex] = sum;
                        outputNeuronsValue[outputNeuronIndex] = Sigmoid(outputNeuronsNetValue[outputNeuronIndex]);
                    }

                    //// Backward propagation
                    // calculate errors and delta errors of the output
                    for (int outputNeuronIndex = 0; outputNeuronIndex < outputNeuronsValue.Length; outputNeuronIndex++)
                    {
                        // TODO: add functionality for more than one neuron at the output.. the current state should work only for expected 1 output neuron (dataRow)
                        outputError[outputNeuronIndex] = testDataOutputs[dataRow, outputNeuronIndex] - outputNeuronsValue[outputNeuronIndex];
                        outputErrorDelta[outputNeuronIndex] = outputError[outputNeuronIndex] * DerivativeSigmoid(outputNeuronsValue[outputNeuronIndex]);
                    }

                    if (z == Epoches)
                    {
                        for (int i = 0; i < numberOfOutputs; i++)
                        {
                            Console.WriteLine("Error: {0}", outputErrorDelta[i]);
                            Console.WriteLine("Output: {0}", outputNeuronsValue[i]);
                            z = 0;
                        }
                    }

                    // calculate errors and delta errors of the LAST hidden layer
                    for (int neuronIndex = 0; neuronIndex < neuronErrors[NumberOfHiddenLayers - 1].Length; neuronIndex++)
                    {
                        // weights[NumberOfHiddenLayers] NumberOfHiddenLayers == the output layer's index
                        neuronErrors[NumberOfHiddenLayers - 1][neuronIndex] = 0;
                        for (int outputErrorIndex = 0; outputErrorIndex < outputError.Length; outputErrorIndex++)
                        {
                            neuronErrors[NumberOfHiddenLayers - 1][neuronIndex] += outputError[outputErrorIndex] * weights[NumberOfHiddenLayers][outputErrorIndex][neuronIndex] * DerivativeSigmoid(neuronValue[NumberOfHiddenLayers - 1][neuronIndex]);
                        }

                        neuronErrorDeltas[NumberOfHiddenLayers - 1][neuronIndex] = neuronErrors[NumberOfHiddenLayers - 1][neuronIndex] * DerivativeSigmoid(neuronValue[NumberOfHiddenLayers - 1][neuronIndex]);
                    }

                    // calculate errors and delta erros of the rest of the hidden layers
                    for (int neuronLayer = NumberOfHiddenLayers - 2; neuronLayer >= 0; neuronLayer--)
                    {
                        for (int neuronIndex = 0; neuronIndex < neuronErrors[neuronLayer].Length; neuronIndex++)
                        {
                            neuronErrors[neuronLayer][neuronIndex] = 0;
                            for (int nextLayerNeuronIndex = 0; nextLayerNeuronIndex < neuronErrors[neuronLayer + 1].Length; nextLayerNeuronIndex++)
                            {
                                neuronErrors[neuronLayer][neuronIndex] += neuronErrors[neuronLayer + 1][nextLayerNeuronIndex] * weights[neuronLayer + 1][nextLayerNeuronIndex][neuronIndex] * DerivativeSigmoid(neuronValue[neuronLayer][neuronIndex]);
                            }

                            neuronErrorDeltas[neuronLayer][neuronIndex] = neuronErrors[neuronLayer][neuronIndex] * DerivativeSigmoid(neuronValue[neuronLayer][neuronIndex]);
                        }
                    }

                    // calculate delta weights of the first hidden layer
                    for (int inputIndex = 0; inputIndex < numberOfInputs; inputIndex++)
                    {
                        for (int currentNeuronIndex = 0; currentNeuronIndex < neuronErrorDeltas[0].Length; currentNeuronIndex++)
                        {
                            deltaWeights[0][currentNeuronIndex][inputIndex] = inputNeurons[inputIndex] * neuronErrorDeltas[0][currentNeuronIndex];
                        }
                    }

                    // calculate delta weights of the rest of the hidden layers
                    for (int currentLayerIndex = 1; currentLayerIndex < neuronValue.GetLength(0); currentLayerIndex++)
                    {
                        for (int inputIndex = 0; inputIndex < neuronErrorDeltas[currentLayerIndex - 1].Length; inputIndex++)
                        {
                            for (int currentNeuronIndex = 0; currentNeuronIndex < neuronErrorDeltas[currentLayerIndex].Length; currentNeuronIndex++)
                            {
                                deltaWeights[currentLayerIndex][currentNeuronIndex][inputIndex] = neuronValue[currentLayerIndex - 1][inputIndex] * neuronErrorDeltas[currentLayerIndex][currentNeuronIndex];
                            }
                        }
                    }

                    // update weights
                    for (int layerIndex = 0; layerIndex < weights.GetLength(0); layerIndex++)
                    {
                        for (int neuronIndex = 0; neuronIndex < weights[layerIndex].GetLength(0); neuronIndex++)
                        {
                            for (int outputIndex = 0; outputIndex < weights[layerIndex][neuronIndex].GetLength(0); outputIndex++)
                            {
                                weights[layerIndex][neuronIndex][outputIndex] += deltaWeights[layerIndex][neuronIndex][outputIndex] * LearningRage;
                            }
                        }
                    }
                }
            }


        }

        private static double[][] SetUpNeurons(int[] hiddenLayersNeuronCounts)
        {
            double[][] neurons = new double[NumberOfHiddenLayers][];
            for (int i = 0; i < hiddenLayersNeuronCounts.Length; i++)
            {
                neurons[i] = new double[hiddenLayersNeuronCounts[i]];
            }

            return neurons;
        }

        private static double[][][] SetUpWeigths(int inputsCount, int outputsCount, int[] hiddenLayersNeuronCounts)
        {
            // hidden layers + output layer
            int totalNumberOflayers = hiddenLayersNeuronCounts.Length + 1;

            // FOR every layer, we need a weight for every neuron of this layer connecting with every neurons/outputs of prev layer 
            // number of Layers, number of neurons in that layer, number of outputs of prev layer
            double[][][] weights = new double[totalNumberOflayers][][];

            int numberOfOutputsInPrevLayer = inputsCount;

            for (int currentLayerIndex = 0; currentLayerIndex < hiddenLayersNeuronCounts.Length; currentLayerIndex++)
            {
                var numberOfNeuronsInCurrentLayer = hiddenLayersNeuronCounts[currentLayerIndex];
                weights[currentLayerIndex] = new double[numberOfNeuronsInCurrentLayer][];

                for (int currentNeuronIndex = 0; currentNeuronIndex < numberOfNeuronsInCurrentLayer; currentNeuronIndex++)
                {
                    weights[currentLayerIndex][currentNeuronIndex] = new double[numberOfOutputsInPrevLayer];

                    // Generate random weights between -0.5 and 0.5
                    for (int i = 0; i < numberOfOutputsInPrevLayer; i++)
                    {
                        weights[currentLayerIndex][currentNeuronIndex][i] = rand.NextDouble() - 0.5;
                    }
                }

                numberOfOutputsInPrevLayer = numberOfNeuronsInCurrentLayer;
            }

            // last layer / output weights
            int lastLayerIndex = weights.Length - 1;
            weights[lastLayerIndex] = new double[outputsCount][];

            for (int currentNeuronIndex = 0; currentNeuronIndex < outputsCount; currentNeuronIndex++)
            {
                weights[lastLayerIndex][currentNeuronIndex] = new double[numberOfOutputsInPrevLayer];

                // Generate random weights between -0.5 and 0.5
                for (int i = 0; i < numberOfOutputsInPrevLayer; i++)
                {
                    weights[lastLayerIndex][currentNeuronIndex][i] = rand.NextDouble() - 0.5;
                }
            }

            return weights;
        }

        private static double[][][] SetUpDeltaWeights(double[][][] weights)
        {
            double[][][] deltaWeights = new double[weights.GetLength(0)][][];

            for (int layerIndex = 0; layerIndex < weights.GetLength(0); layerIndex++)
            {
                deltaWeights[layerIndex] = new double[weights[layerIndex].GetLength(0)][];

                for (int neuronIndex = 0; neuronIndex < weights[layerIndex].Length; neuronIndex++)
                {
                    deltaWeights[layerIndex][neuronIndex] = new double[weights[layerIndex][neuronIndex].Length];
                }
            }

            return deltaWeights;
        }

        private static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-1 * x));
        }

        private static double DerivativeSigmoid(double x)
        {
            return x * (1 - x);
        }
    }
}
