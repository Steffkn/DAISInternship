namespace Tester
{
    using NNExternal;
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
                    { 0, 0, 0 },
                    { 0, 0, 1 },
                    { 0, 1, 0 },
                    { 0, 1, 1 },
                    { 1, 0, 0 },
                    { 1, 0, 1 },
                    { 1, 1, 0 },
                    { 1, 1, 1 }
                };

            float[,] outputs = new float[,] {
                { 0, 0},
                { 0, 1},
                { 0, 0},
                { 0, 1},
                { 0, 0},
                { 0, 1},
                { 1, 1},
                { 1, 1}
            };

            NeuralNetwork net = new NeuralNetwork(new int[] { inputs.GetLength(1), 25, 25, outputs.GetLength(1) }); //intiilize network
            net.Learn(inputs, outputs, 10000);


            //output to see if the network has learnt
            //WHICH IT HAS!!!!!

            for (int j = 0; j < inputs.GetLength(0); j++)
            {
                var input = Helpers.GetRow(inputs, j);
                var expected = Helpers.GetRow(outputs, j);
                var result = net.FeedForward(input);
                Console.Write("Expected: {0} {1} -> ", expected[0], expected[1]);

                foreach (var item in result)
                {
                    Console.Write($"{item:F5}, ");
                }
                Console.WriteLine();
            }
        }
    }
}