// x neurons in input layer
// 3 neurons - 1 hidden layer
// 1 neuron - 1 output layer

namespace NeuralNetworkXORTake2
{
    using MindLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class NeuralNetworkXORTake2
    {
        static void Main()
        {
            const int iterations = 10000;
            double[][] inputs = {
                new double[]{ 0, 0 },
                new double[]{ 0, 1 },
                new double[]{ 1, 0 },
                new double[]{ 1, 1 }
            };

            double[][] outputs = {
                new double[]{ 0, 0 },
                new double[]{ 1, -1 },
                new double[]{ 1, -1 },
                new double[]{ 1, 1 }
            };

            var mind = new MindTest();

            mind.SetUpTrainingData(inputs, outputs);
            mind.SetUpLayers();
            mind.SetUpSynapses();

            while (true)
            {
                mind.Train(iterations);
                double a = double.Parse(Console.ReadLine());
                double b = double.Parse(Console.ReadLine());
                Console.WriteLine(string.Join(" ", mind.GetResult(new List<double>() { a, b })));

                foreach (var layer in mind.Neurons)
                {
                    foreach (var neuron in layer)
                    {
                        Console.WriteLine(neuron);
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void Test(double[][] weights, double[][] deltaWeights)
        {
            var input = new int[Mind.Inputs.GetLength(1)];

            Console.WriteLine("Test the network");
            for (int x = 0; x < input.Length; x++)
            {
                Console.Write($"x{x} = ");
                string inputString = Console.ReadLine();
                input[x] = int.Parse(inputString == string.Empty ? "0" : inputString);
            }

            // forward propagation
            Mind.ForwardPropagation(weights, input);

            foreach (var neuron in Mind.Neurons[0])
            {
                Console.WriteLine(" ===== Error: {0:F6}", neuron.DeltaErrors);
                Console.WriteLine(" ===== Output: {0:F6}", neuron.NeuronSum);
            }
        }


        private static void OldTest()
        {
            string fileName = @"inputs\AND.txt";
            const int iterations = 10000;

            try
            {
                string[] fileInput = File.ReadAllLines(fileName);
                Mind.Inputs = MemoryManager.LoadInput(fileInput);
                Mind.Outputs = MemoryManager.LoadOutput(fileInput);
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

                Mind.Outputs = new int[,] {
                    { 0 },
                    { 1 },
                    { 0 },
                    { 1 },
                    { 0 },
                    { 1 },
                    { 1 },
                    { 1 }
                };
            }

            int numberOfinputNeurons = Mind.Inputs.GetLength(1);
            int numberOfoutputNeurons = Mind.Outputs.GetLength(1);
            int numberOfhiddenNeuronsPerLayer = 3;
            int numberOfLayers = 1;

            Mind.Neurons.Add(new List<Neuron>());

            // add one more for inputs maybe?
            // outputs at index 0
            for (int i = 0; i < numberOfoutputNeurons; i++)
            {
                Mind.Neurons[0].Add(new Neuron());
            }

            // hiddne layers after 1 index
            for (int layerIndex = 0; layerIndex < numberOfLayers; layerIndex++)
            {
                Mind.Neurons.Add(new List<Neuron>());
                for (int i = 0; i < numberOfhiddenNeuronsPerLayer; i++)
                {
                    Mind.Neurons[layerIndex + 1].Add(new Neuron());
                }
            }

            // +1 for the output
            int weightRows = numberOfhiddenNeuronsPerLayer + 1;

            double[][] weights = new double[weightRows][];
            double[][] deltaWeights = new double[weightRows][];

            try
            {
                MemoryManager.LoadWeights(fileName, weights);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File reading exception! \n{0}", ex.Message);
                Console.WriteLine("Loading random weights...");
                Mind.LoadRandomWeights(numberOfinputNeurons, numberOfoutputNeurons, numberOfhiddenNeuronsPerLayer, weightRows, weights);
            }

            for (int x = 0; x < weights.GetLength(0); x++)
            {
                deltaWeights[x] = new double[weights[x].Length];
            }

            while (true)
            {
                Mind.Train(fileName, iterations, weights, deltaWeights);
                Test(weights, deltaWeights);
            }
        }

    }
}
