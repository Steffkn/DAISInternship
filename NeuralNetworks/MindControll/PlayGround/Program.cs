using System;
using System.Linq;
using Brain;

namespace PlayGround
{
    class Program
    {
        static void Main(string[] args)
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
                { 0, 0, 0, 1,  0, 0, 0, 1 },
                { 0, 0, 1, 0,  0, 0, 1, 0 },
                { 0, 1, 0, 0,  0, 1, 0, 0 },
                { 1, 0, 0, 0,  1, 0, 0, 0 },
                { 0, 0, 0, 1,  1, 1, 1, 0 },
                { 0, 0, 1, 0,  1, 1, 0, 1 },
                { 0, 1, 0, 0,  1, 0, 1, 1 },
                { 1, 0, 0, 0,  0, 1, 1, 1 },
            };

            float[,] outputs = new float[,] {
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 1, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 },
            };

           //  float[,] outputs = new float[,] {
           //      { 0, 0, 1, 1 },
           //      { 1, 0, 0, 0 },
           //      { 0, 1, 0, 1 },
           //      { 1, 0, 0, 0 },
           //      { 0, 1, 0, 1 },
           //      { 0, 0, 1, 1 },
           //      { 0, 1, 0, 1 },
           //      { 0, 0, 1, 1 },
           //      { 1, 0, 0, 0},
           //  };
            //intiilize network
            NeuralNetwork net = new NeuralNetwork(new int[] { inputs.GetLength(1), 25, 25, outputs.GetLength(1) }, true);
            net.Learn(inputs, outputs, 10000);

            var input = string.Empty;
            while ((input = Console.ReadLine()) != "end")
            {
                float[] income = input.Split(' ').Select(float.Parse).ToArray();
                var result = net.FeedForward(income);

                foreach (var item in result)
                {
                    Console.Write($"{item:F0} ");
                }

                Console.WriteLine();
            }
        }
    }
}
