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
        static void Main()
        {
            string fileName = @"inputs\AND.txt";
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
                Mind.LoadRandomWeights(numberOfinputNeurons, numberOfoutputNeurons, numberOfhiddenNeurons, weightRows, weights);
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

            while (true)
            {
                Train(fileName, iterations, weights, deltaWeights);
                Test(weights, deltaWeights);
            }
        }

        public static void Test(double[][] weights, double[][] deltaWeights)
        {
            var input = new int[Mind.Inputs.GetLength(1)];

            for (int i = 0; i < Mind.Inputs.GetLength(0); i++)
            {
                Console.WriteLine("Test the network");
                for (int x = 0; x < input.Length; x++)
                {
                    Console.Write($"x{x} = ");
                    string inputString = Console.ReadLine();
                    input[x] = int.Parse(inputString == string.Empty ? "0" : inputString);
                }

                // forward propagation
                Mind.ForwardPropagation(weights, input);

                // simple backpropagation
                Mind.BackwardPropagation(Mind.Outputs[i], weights, deltaWeights, input);

                Console.WriteLine(" ===== Error: {0:F6}", Mind.Neurons[0].DeltaErrors);
                Console.WriteLine(" ===== Prediction: {0:F6}", Mind.Neurons[0].NeuronSum);
            }
        }

        public static void Train(string fileName, int iterations, double[][] weights, double[][] deltaWeights)
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
                    Mind.BackwardPropagation(Mind.Outputs[i], weights, deltaWeights, input);
                }
            }

            WriteToFile(fileName, weights);
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
            string[] lineText = fileInput[line].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
            int inputLenghtX = int.Parse(lineText[0]);
            int inputLenghtY = int.Parse(lineText[1]);

            int[,] result = new int[inputLenghtX, inputLenghtY];

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
