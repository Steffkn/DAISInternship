namespace MindLib
{
    using System;
    using System.Collections.Generic;

    public static class Mind
    {
        public const double Learning_Rate = 0.1;

        private static List<Neuron> neurons = new List<Neuron>();
        private static List<Synapse> synapses = new List<Synapse>();
        private static int[,] inputs;
        private static int[] outputs;

        public static List<Neuron> Neurons { get => neurons; set => neurons = value; }
        public static List<Synapse> Synapses { get => synapses; set => synapses = value; }
        public static int[,] Inputs { get => inputs; set => inputs = value; }
        public static int[] Outputs { get => outputs; set => outputs = value; }

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


    public enum ActivatorType
    {
        Sigmoid,
        HTan
    }
}
