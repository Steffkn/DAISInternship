namespace MindLib
{
    using System;
    using System.Collections.Generic;

    public static class Mind
    {
        // we can add seed later
        private static Random Rand = new Random();

        public const double Learning_Rate = 0.1;

        public static List<Neuron> Neurons { get; set; } = new List<Neuron>();

        public static List<Synapse> Synapses { get; set; } = new List<Synapse>();

        public static int[,] Inputs { get; set; }

        public static int[,] Outputs { get; set; }

        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-1 * x));
        }

        public static double DerivativeSigmoid(double x)
        {
            return x * (1 - x);
        }

        public static double TanH(double x)
        {
            return (2 / (1 + Math.Exp(-2 * x))) - 1;
        }

        public static double DerivativeTanH(double x)
        {
            return 1 - (x * x);
        }

        public static double ReLU(double x)
        {
            return Math.Max(0.0, x);
        }

        public static void Train(string outputFileName, int iterations, double[][] weights, double[][] deltaWeights)
        {
            for (int z = 0; z <= iterations; z++)
            {
                var input = new int[Mind.Inputs.GetLength(1)];

                for (int i = 0; i < Mind.Inputs.GetLength(0); i++)
                {
                    for (int x = 0; x < input.Length; x++)
                    {
                        input[x] = Mind.Inputs[i, x];
                    }

                    // forward propagation
                    Mind.ForwardPropagation(weights, input);

                    // simple backpropagation
                    Mind.BackwardPropagation(i, weights, deltaWeights, input);
                }
            }

            MemoryManager.WriteToFile(outputFileName, weights);
        }

        public static void ForwardPropagation(double[][] weights, int[] input)
        {
            for (int net = 1; net < Mind.Neurons.Count; net++)
            {
                Mind.Neurons[net].NetSum = 0;
                for (int index = 0; index < input.Length; index++)
                {
                    Mind.Neurons[net].NetSum += input[index] * weights[index][net - 1];
                }

                Mind.Neurons[net].NeuronSum = Mind.Activation(Mind.Neurons[net].NetSum);
            }

            // output neuron is index 0
            Mind.Neurons[0].NetSum = 0;
            for (int neuronIndex = 1; neuronIndex < Mind.Neurons.Count; neuronIndex++)
            {
                Mind.Neurons[0].NetSum += Mind.Neurons[neuronIndex].NeuronSum * weights[neuronIndex + 1][0];
            }

            Mind.Neurons[0].NeuronSum = Mind.Activation(Mind.Neurons[0].NetSum);
        }

        public static void BackwardPropagation(int target, double[][] weights, double[][] deltaWeights, int[] input)
        {
            // output errors
            Mind.Neurons[0].TotalErrors = Mind.Outputs[target, 0] - Mind.Neurons[0].NeuronSum;
            Mind.Neurons[0].DeltaErrors = Mind.Neurons[0].TotalErrors * Mind.Derivative(Mind.Neurons[0].NeuronSum);

            // hidden errors
            for (int error = 1; error < Mind.Neurons.Count; error++)
            {
                Mind.Neurons[error].TotalErrors = Mind.Neurons[0].TotalErrors * weights[error + 1][0] * Mind.Derivative(Mind.Neurons[error].NeuronSum);
                Mind.Neurons[error].DeltaErrors = Mind.Neurons[error].TotalErrors * Mind.Derivative(Mind.Neurons[error].NeuronSum);
            }

            // number of neurons till output 2
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < deltaWeights[x].Length; y++)
                {
                    deltaWeights[x][y] = input[x] * Mind.Neurons[y + 1].DeltaErrors;
                }
            }

            deltaWeights[2][0] = Mind.Neurons[1].NeuronSum * Mind.Neurons[0].DeltaErrors;
            deltaWeights[3][0] = Mind.Neurons[2].NeuronSum * Mind.Neurons[0].DeltaErrors;
            deltaWeights[4][0] = Mind.Neurons[3].NeuronSum * Mind.Neurons[0].DeltaErrors;

            CalculateNewWeights(weights, deltaWeights);
        }

        public static void CalculateNewWeights(double[][] weights, double[][] deltaWeights)
        {
            for (int x = 0; x < weights.GetLength(0); x++)
            {
                for (int y = 0; y < weights[x].GetLength(0); y++)
                {
                    weights[x][y] += deltaWeights[x][y] * Mind.Learning_Rate;
                }
            }
        }

        public static double Activation(double sum)
        {
            return TanH(sum);
        }

        public static double Derivative(double sum)
        {
            return DerivativeTanH(sum);
        }

        public static void LoadRandomWeights(int inputNeurons, int outputNeurons, int hiddenNeurons, int weightRows, double[][] weights)
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
    }

    public enum ActivatorType
    {
        Sigmoid,
        HTan
    }
}
