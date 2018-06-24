using System;
using System.Numerics;

namespace Take3
{
    class Program
    {
        static void Main()
        {
            int[,] inputs = {
                { 0, 0 },
                { 0, 1 },
                { 1, 0 },
                { 1, 1 }
            };

            int[] outputs = { 0, 0, 0, 1 };

            // 2 neurons - 1 input layer
            // 3 neurons - 1 hidden layer
            // 1 neuron - 1 output layer

            double[][] weights = new double[2][];
            double[][] deltaWeights = new double[2][];

            weights[0] = new double[] { 0.8, 0.4, 0.3, 0.2, 0.9, 0.5 };
            weights[1] = new double[] { 0.3, 0.5, 0.9, };

            deltaWeights[0] = new double[6];
            deltaWeights[1] = new double[3];

            double net1 = 0;
            double net2 = 0;
            double net3 = 0;
            double netO = 0;
            double hO = 0;
            double h1 = 0;
            double h2 = 0;
            double h3 = 0;

            double output_error = 0;
            double output_errorDelta = 0;
            double hidden_error1 = 0;
            double hidden_errorDelta1 = 0;
            double hidden_error2 = 0;
            double hidden_errorDelta2 = 0;
            double hidden_error3 = 0;
            double hidden_errorDelta3 = 0;

            for (int z = 0; z <= 100000; z++)
            {
                var input = new int[2];

                for (int i = 0; i < inputs.GetLength(0); i++)
                {
                    if (z == 100000)
                    {
                        input[0] = int.Parse(Console.ReadLine());
                        input[1] = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        input[0] = inputs[i, 0];
                        input[1] = inputs[i, 1];
                    }

                    // forward propagation
                    net1 = input[0] * weights[0][0] + input[1] * weights[0][3];
                    net2 = input[0] * weights[0][1] + input[1] * weights[0][4];
                    net3 = input[0] * weights[0][2] + input[1] * weights[0][5];
                    h1 = Sigmoid(net1);
                    h2 = Sigmoid(net2);
                    h3 = Sigmoid(net3);

                    netO = h1 * weights[1][0] + h2 * weights[1][1] + h3 * weights[1][2];
                    hO = Sigmoid(netO);


                    // simple backpropagation
                    output_error = outputs[i] - hO;
                    output_errorDelta = output_error * DerivativeSigmoid(hO);

                    if (z == 100000)
                    {
                        Console.WriteLine("Error: {0}", output_errorDelta);
                        Console.WriteLine("Output: {0}", hO);
                        z = 0;
                    }

                    hidden_error1 = output_error * weights[1][0] * DerivativeSigmoid(h1);
                    hidden_errorDelta1 = hidden_error1 * DerivativeSigmoid(h1);

                    hidden_error2 = output_error * weights[1][1] * DerivativeSigmoid(h2);
                    hidden_errorDelta2 = hidden_error2 * DerivativeSigmoid(h2);

                    hidden_error3 = output_error * weights[1][2] * DerivativeSigmoid(h3);
                    hidden_errorDelta3 = hidden_error3 * DerivativeSigmoid(h3);

                    deltaWeights[0][0] = input[0] * hidden_errorDelta1;
                    deltaWeights[0][1] = input[0] * hidden_errorDelta2;
                    deltaWeights[0][2] = input[0] * hidden_errorDelta3;
                    deltaWeights[0][3] = input[1] * hidden_errorDelta1;
                    deltaWeights[0][4] = input[1] * hidden_errorDelta2;
                    deltaWeights[0][5] = input[1] * hidden_errorDelta3;

                    deltaWeights[1][0] = h1 * output_errorDelta;
                    deltaWeights[1][1] = h2 * output_errorDelta;
                    deltaWeights[1][2] = h3 * output_errorDelta;

                    weights[0][0] += deltaWeights[0][0] * 0.1;
                    weights[0][1] += deltaWeights[0][1] * 0.1;
                    weights[0][2] += deltaWeights[0][2] * 0.1;
                    weights[0][3] += deltaWeights[0][3] * 0.1;
                    weights[0][4] += deltaWeights[0][4] * 0.1;
                    weights[0][5] += deltaWeights[0][5] * 0.1;
                    weights[1][0] += deltaWeights[1][0] * 0.1;
                    weights[1][1] += deltaWeights[1][1] * 0.1;
                    weights[1][2] += deltaWeights[1][2] * 0.1;
                }
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
