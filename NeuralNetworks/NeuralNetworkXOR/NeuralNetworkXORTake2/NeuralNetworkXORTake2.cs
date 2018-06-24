// x neurons in input layer
// 3 neurons - 1 hidden layer
// 1 neuron - 1 output layer

namespace NeuralNetworkXORTake2
{
    using MindLib;
    using System;
    using System.IO;
    using System.Text;

    public class NeuralNetworkXORTake2
    {
        // we can add seed later
        private static Random Rand = new Random();

        static void Main()
        {
            string fileName = @".\inputs\AND.txt";
            const int iterations = 10000;

            try
            {
                string[] fileInput = File.ReadAllLines(fileName);
                Mind.Inputs = LoadInput(fileInput);
                Mind.Outputs = LoadOutput(fileInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File reading exception! \n{0}", ex.Message);
                Mind.Inputs = new int[,]{
                    { 0, 0, 0 },
                    { 0, 0, 1 },
                    { 0, 1, 0 },
                    { 0, 1, 1 },
                    { 1, 0, 0 },
                    { 1, 0, 1 },
                    { 1, 1, 0 },
                    { 1, 1, 1 }
                };

                Mind.Outputs = new int[] { 0, 1, 0, 1, 0, 1, 1, 1 };
            }

            int numberOfinputNeurons = Mind.Inputs.GetLength(1);
            int numberOfoutputNeurons = 1;
            int numberOfhiddenNeurons = 3;

            // +1 for the output
            int weightRows = Mind.Inputs.GetLength(0) + 1;

            double[][] weights = new double[weightRows][];
            double[][] deltaWeights = new double[weightRows][];

            try
            {
                LoadWeights(fileName, weights);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File reading exception! \n{0}", ex.Message);
                Console.WriteLine("Loading random weights...");
                LoadRandomWeights(numberOfinputNeurons, numberOfoutputNeurons, numberOfhiddenNeurons, weightRows, weights);
            }

            for (int x = 0; x < weights.GetLength(0); x++)
            {
                deltaWeights[x] = new double[weights[x].Length];
            }

            // add one more for inputs maybe?
            for (int i = 0; i < numberOfhiddenNeurons + numberOfoutputNeurons; i++)
            {
                Mind.Neurons.Add(new Neuron());
            }

            for (int z = 0; z <= iterations; z++)
            {
                var input = new int[Mind.Inputs.GetLength(1)];

                for (int i = 0; i < Mind.Inputs.GetLength(0); i++)
                {
                    if (z == iterations)
                    {
                        Console.WriteLine("Test the network");
                        for (int x = 0; x < input.Length; x++)
                        {
                            Console.Write($"x{x} = ");
                            string inputString = Console.ReadLine();
                            input[x] = int.Parse(inputString == string.Empty ? "0" : inputString);
                        }
                    }
                    else
                    {
                        for (int x = 0; x < input.Length; x++)
                        {
                            input[x] = Mind.Inputs[i, x];
                        }
                    }

                    // forward propagation
                    ForwardPropagation(weights, input);

                    // simple backpropagation
                    BackwardPropagation(Mind.Outputs[i], weights, deltaWeights, input);

                    if (z == iterations)
                    {
                        Console.WriteLine(" ===== Error: {0:F6}", Mind.Neurons[0].DeltaErrors);
                        Console.WriteLine(" ===== Prediction: {0:F6}", Mind.Neurons[0].NeuronSum);

                        WriteToFile(fileName, weights);
                        z = 0;
                    }
                }
            }
        }

        private static void LoadWeights(string fileName, double[][] weights)
        {
            string[] fileInput = File.ReadAllLines(fileName);

            int line = Mind.Inputs.GetLength(0) + 2;

            for (int x = 0; x < weights.GetLength(0); x++)
            {
                string[] lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                weights[x] = new double[lineText.Length];

                for (int y = 0; y < weights[x].Length; y++)
                {
                    weights[x][y] = double.Parse(lineText[y]);
                }

                line++;
            }

            Console.WriteLine("Weights loaded from file {0}", fileName);
        }

        private static int[,] LoadInput(string[] fileInput)
        {
            int line = 0;
            int[,] result;
            string[] lineText = fileInput[line].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
            int inputLenghtX = int.Parse(lineText[0]);
            int inputLenghtY = int.Parse(lineText[1]);

            result = new int[inputLenghtX, inputLenghtY];

            for (int i = 0; i < inputLenghtX; i++)
            {
                line++;
                lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int y = 0; y < inputLenghtY; y++)
                {
                    result[i, y] = int.Parse(lineText[y]);
                }
            }

            return result;
        }

        private static int[] LoadOutput(string[] fileInput)
        {
            int[] result;
            string[] lineText = fileInput[0].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);

            int inputLenghtX = int.Parse(lineText[0]);
            int outputLenght = int.Parse(lineText[2]);
            int line = inputLenghtX + 1;

            result = new int[outputLenght];

            lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int y = 0; y < outputLenght; y++)
            {
                result[y] = int.Parse(lineText[y]);
            }

            return result;
        }

        private static void LoadRandomWeights(int inputNeurons, int outputNeurons, int hiddenNeurons, int weightRows, double[][] weights)
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

        private static void BackwardPropagation(int target, double[][] weights, double[][] deltaWeights, int[] input)
        {
            // output errors
            Mind.Neurons[0].TotalErrors = target - Mind.Neurons[0].NeuronSum;
            Mind.Neurons[0].DeltaErrors = Mind.Neurons[0].TotalErrors * Mind.DerivativeTanH(Mind.Neurons[0].NeuronSum);

            // hidden errors
            for (int error = 1; error < Mind.Neurons.Count; error++)
            {
                Mind.Neurons[error].TotalErrors = Mind.Neurons[0].TotalErrors * weights[error + 1][0] * Mind.DerivativeTanH(Mind.Neurons[error].NeuronSum);
                Mind.Neurons[error].DeltaErrors = Mind.Neurons[error].TotalErrors * Mind.DerivativeTanH(Mind.Neurons[error].NeuronSum);
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

        private static void CalculateNewWeights(double[][] weights, double[][] deltaWeights)
        {
            for (int x = 0; x < weights.GetLength(0); x++)
            {
                for (int y = 0; y < weights[x].GetLength(0); y++)
                {
                    weights[x][y] += deltaWeights[x][y] * Mind.Learning_Rate;
                }
            }
        }

        private static void ForwardPropagation(double[][] weights, int[] input)
        {
            for (int net = 1; net < Mind.Neurons.Count; net++)
            {
                Mind.Neurons[net].NetSum = 0;
                for (int index = 0; index < input.Length; index++)
                {
                    Mind.Neurons[net].NetSum += input[index] * weights[index][net - 1];
                }

                Mind.Neurons[net].NeuronSum = Mind.TanH(Mind.Neurons[net].NetSum);
            }

            // output neuron is index 0
            Mind.Neurons[0].NetSum = 0;
            for (int neuronIndex = 1; neuronIndex < Mind.Neurons.Count; neuronIndex++)
            {
                Mind.Neurons[0].NetSum += Mind.Neurons[neuronIndex].NeuronSum * weights[neuronIndex + 1][0];
            }

            Mind.Neurons[0].NeuronSum = Mind.TanH(Mind.Neurons[0].NetSum);
        }


        public static void WriteToFile(string fileName, double[][] weights)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter writetext = new StreamWriter(stream))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}x{1}x{2}\n", Mind.Inputs.GetLength(0), Mind.Inputs.GetLength(1), Mind.Outputs.Length);

                    for (int i = 0; i < Mind.Inputs.GetLength(0); i++)
                    {
                        for (int y = 0; y < Mind.Inputs.GetLength(1) - 1; y++)
                        {
                            sb.Append($"{Mind.Inputs[i, y]} ");
                        }

                        sb.Append($"{Mind.Inputs[i, Mind.Inputs.GetLength(1) - 1]}");
                        sb.AppendLine();
                    }

                    for (int i = 0; i < Mind.Outputs.Length - 1; i++)
                    {
                        sb.Append($"{Mind.Outputs[i]} ");
                    }

                    sb.Append($"{Mind.Outputs[Mind.Outputs.Length - 1]}");
                    sb.AppendLine();


                    for (int x = 0; x < weights.GetLength(0); x++)
                    {
                        for (int y = 0; y < weights[x].GetLength(0) - 1; y++)
                        {
                            sb.Append($"{weights[x][y]} ");
                        }

                        sb.Append($"{weights[x][weights[x].Length - 1]}");
                        sb.AppendLine();
                    }

                    writetext.Write(sb.ToString());
                }
            }
        }
    }
}
