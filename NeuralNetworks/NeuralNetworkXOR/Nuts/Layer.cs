using Nuts.TransferFunctions;
using System;

namespace Nuts
{
    public class Layer
    {
        private int numberOfInputs;
        private int numberOfOutputs;

        public double[] outputs;
        private double[] inputs;
        public double[,] weights;
        private double[,] weightsDelta;
        public double[] gamma;
        private double[] error;
        private static Random rand = new Random();
        private double LearningRate = 0.033f;

        public Layer(int numberOfInputs, int numberOfOutputs)
        {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOutputs = numberOfOutputs;

            this.outputs = new double[numberOfOutputs];
            this.inputs = new double[numberOfInputs];
            this.weights = new double[numberOfOutputs, numberOfInputs];
            this.weightsDelta = new double[numberOfOutputs, numberOfInputs];
            this.gamma = new double[numberOfOutputs];
            this.error = new double[numberOfOutputs];
            this.InitRandomWeights();
        }

        public double[] FeedForward(double[] inputs, ITransferFunction transferFunction)
        {
            this.inputs = inputs;

            for (int i = 0; i < numberOfOutputs; i++)
            {
                this.outputs[i] = 0;
                for (int j = 0; j < numberOfInputs; j++)
                {
                    this.outputs[i] += this.inputs[j] * this.weights[i, j];
                }

                outputs[i] = transferFunction.Activate(outputs[i]);
            }

            return this.outputs;
        }

        public void InitRandomWeights()
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] = (double)rand.NextDouble() - 0.5f;
                }
            }
        }

        public void UpdateWeights()
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] -= weightsDelta[i, j] * LearningRate;
                }
            }
        }

        public void BackwardPropagationOutput(double[] expected, ITransferFunction transferFunction)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                error[i] = outputs[i] - expected[i];
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                gamma[i] = error[i] * transferFunction.Derivative(outputs[i]);
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }
        }

        public void BackwardPropagationHidden(double[] gammaForward, double[,] weightsForward, ITransferFunction transferFunction)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                gamma[i] = 0;

                for (int j = 0; j < gammaForward.Length; j++)
                {
                    gamma[i] += gamma[j] * weightsForward[j, i];
                }

                gamma[i] *= transferFunction.Derivative(outputs[i]);
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }
        }

        //public double TanH(double x)
        //{
        //    return (2 / (1 + Math.Exp(-2 * x))) - 1;
        //}

        //public double TanHDerivative(double value)
        //{
        //    return 1 - (value * value);
        //}

        //public double Sigmoid(double x)
        //{
        //    return 1 / (1 + Math.Exp(-1 * x));
        //}

        //public double DerivativeSigmoid(double x)
        //{
        //    return x * (1 - x);
        //}


        //public double DerivativeTanH(double x)
        //{
        //    return 1 - (x * x);
        //}

        //public static double ReLU(double x)
        //{
        //    return Math.Max(0.0, x);
        //}

        //public static double DerivativeReLU(double x)
        //{
        //    return x > 0 ? 1 : 0;
        //}
    }
}
