namespace Tester
{
    using Brain;
    using Helpers;
    using System;

    public class Tester
    {
        // Use this for initialization
        public static void Main()
        {
            // 0 0 0    => 0
            // 0 0 1    => 1
            // 0 1 0    => 1
            // 0 1 1    => 0
            // 1 0 0    => 1
            // 1 0 1    => 0
            // 1 1 0    => 0
            // 1 1 1    => 1

            float[,] inputs = new float[,]{
                    { 0, 0 },
                    { 0, 1 },
                    { 1, 0 },
                    { 1, 1 },
                };

            float[,] outputs = new float[,] {
                    { 0 },
                    { 1 },
                    { 1 },
                    { 1 },
            };

            // intiilize network
            // NeuralNetwork net = new NeuralNetwork(new int[] { inputs.GetLength(1), 25, 25, outputs.GetLength(1) }, true);
            // net.Learn(inputs, outputs, 10000);

            NeuralNetwork net = new NeuralNetwork("xor");

            var result = net.FeedForward(new float[] { 1, 1 });

            foreach (var item in result)
            {
                Console.Write($"{item:F0} ");
            }

            Console.WriteLine();
            //ChechResults(inputs, outputs, net);
        }

        private static void ChechResults(float[,] inputs, float[,] outputs, NeuralNetwork net)
        {
            //output to see if the network has learnt
            for (int j = 0; j < inputs.GetLength(0); j++)
            {
                var input = ArrayHelper.GetRow(inputs, j);
                var expected = ArrayHelper.GetRow(outputs, j);
                var result = net.FeedForward(input);

                Console.Write("Expected: ");
                for (int i = 0; i < expected.Length; i++)
                {
                    Console.Write("{0} ", expected[i]);
                }

                Console.Write("-> ");

                for (int i = 0; i < result.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (expected[i] - result[i] < 0.005)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }

                    Console.Write($" {result[i]:F5} ");
                }

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}