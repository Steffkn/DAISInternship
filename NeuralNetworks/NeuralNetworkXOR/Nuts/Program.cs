using System;
using System.Linq;

namespace Nuts
{
    class Program
    {
        static void Main(string[] args)
        {

            var inputs = new double[][]{
                    new double[]{ 0, 0, 0 },
                    new double[]{ 0, 0, 1 },
                    new double[]{ 0, 1, 0 },
                    new double[]{ 0, 1, 1 },
                    new double[]{ 1, 0, 0 },
                    new double[]{ 1, 0, 1 },
                    new double[]{ 1, 1, 0 },
                    new double[]{ 1, 1, 1 }
                };

            var outputs = new double[][] {
                   new double[]{ 0 },
                   new double[]{ 1 },
                   new double[]{ 0 },
                   new double[]{ 1 },
                   new double[]{ 0 },
                   new double[]{ 1 },
                   new double[]{ 1 },
                   new double[]{ 1 }
                };

            int numberOfInputs = inputs[0].Length;
            int numberOfOutputs = outputs[0].Length;
            int[] numberOfNeurosInHiddenLayers = new int[] { 25, 25, };
            int totalNumberOflayers = numberOfNeurosInHiddenLayers.Length + 2;

            int[] inputInformation = new int[totalNumberOflayers];

            for (int i = 1; i < totalNumberOflayers - 1; i++)
            {
                inputInformation[i] = numberOfNeurosInHiddenLayers[i - 1];
            }
            inputInformation[0] = numberOfInputs;
            inputInformation[totalNumberOflayers - 1] = numberOfOutputs;
            NeuralNetwork net = new NeuralNetwork(inputInformation);

            while (true)
            {
                for (int i = 0; i < 10000; i++)
                {
                    for (int dataRow = 0; dataRow < inputs.Length; dataRow++)
                    {
                        var ins = new double[inputs[dataRow].Length];
                        for (int dataCol = 0; dataCol < ins.Length; dataCol++)
                        {
                            ins[dataCol] = inputs[dataRow][dataCol];
                        }

                        var expected = new double[outputs[dataRow].Length];
                        for (int dataCol = 0; dataCol < expected.Length; dataCol++)
                        {
                            expected[dataCol] = outputs[dataRow][dataCol];
                        }

                        net.FeedForward(ins);
                        net.BackwardPropagation(expected);
                    }
                }

                for (int i = 0; i < inputs.Length; i++)
                {
                    double[] result = net.FeedForward(inputs[i]);

                    Console.WriteLine("Inputs  {0:f6}", string.Join(" ", inputs[i]));
                    Console.WriteLine("Expects {0:f6}", string.Join(" ", outputs[i]));
                    Console.WriteLine("\tResult \t{0:f6}", string.Join(" ", result));
                    double[] errors = new double[outputs[i].Length];

                    for (int errorIndex = 0; errorIndex < errors.Length; errorIndex++)
                    {
                        errors[errorIndex] = outputs[i][errorIndex] - result[errorIndex];
                    }

                    Console.WriteLine("\tError: \t{0:f6}", string.Join(" ", errors));
                    Console.WriteLine();
                }

                Console.Write("Continue...");
                HandTest(net);
            }
        }

        private static void HandTest(NeuralNetwork net)
        {
            Console.Write("Enter 3 numbers for XOR:  ");
            double[] values = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            var result = net.FeedForward(values);
            Console.WriteLine("Result {0}", string.Join(" ", result));
        }
    }
}
