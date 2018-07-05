using System;
using System.Linq;

namespace Nuts
{
    class Program
    {
        static void Main(string[] args)
        {

            var inputs = new int[][]{
                    new int[]{ 0, 0, 0 },
                    new int[]{ 0, 0, 1 },
                    new int[]{ 0, 1, 0 },
                    new int[]{ 0, 1, 1 },
                    new int[]{ 1, 0, 0 },
                    new int[]{ 1, 0, 1 },
                    new int[]{ 1, 1, 0 },
                    new int[]{ 1, 1, 1 }
                };

            var outputs = new int[][] {
                   new int[]{ 0 },
                   new int[]{ 1 },
                   new int[]{ 0 },
                   new int[]{ 1 },
                   new int[]{ 0 },
                   new int[]{ 1 },
                   new int[]{ 1 },
                   new int[]{ 1 }
                };

            NeuralNetwork net = new NeuralNetwork(new int[] { 3, 25, 25, 1 });

            while (true)
            {
                for (int i = 0; i < 5000; i++)
                {
                    for (int dataRow = 0; dataRow < inputs.Length; dataRow++)
                    {
                        var ins = new float[inputs[dataRow].Length];
                        for (int dataCol = 0; dataCol < ins.Length; dataCol++)
                        {
                            ins[dataCol] = inputs[dataRow][dataCol];
                        }

                        var expected = new float[outputs[dataRow].Length];
                        for (int dataCol = 0; dataCol < expected.Length; dataCol++)
                        {
                            expected[dataCol] = outputs[dataRow][dataCol];
                        }

                        net.FeedForward(ins);
                        net.BackwardPropagation(expected);
                    }
                }

                Console.Write("Enter 3 numbers for XOR:  ");
                float[] values = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(float.Parse)
                    .ToArray();


                var result = net.FeedForward(values);
                Console.WriteLine("Result {0}", string.Join(" ", result));
            }
        }

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
    }
}
