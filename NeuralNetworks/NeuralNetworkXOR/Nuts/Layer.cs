using System;
using System.Collections.Generic;
using System.Text;

namespace Nuts
{
    public class Layer
    {
        private int numberOfInputs;
        private int numberOfOutputs;

        public float[] outputs;
        private float[] inputs;
        public float[,] weights;
        private float[,] weightsDelta;
        public float[] gamma;
        private float[] error;
        private static Random rand = new Random();
        private float LearningRate = 0.003f;

        public Layer(int numberOfInputs, int numberOfOutputs)
        {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOutputs = numberOfOutputs;

            this.outputs = new float[numberOfOutputs];
            this.inputs = new float[numberOfInputs];
            this.weights = new float[numberOfOutputs, numberOfInputs];
            this.weightsDelta = new float[numberOfOutputs, numberOfInputs];
            this.gamma = new float[numberOfOutputs];
            this.error = new float[numberOfOutputs];
            this.InitRandomWeights();
        }

        public float[] FeedForward(float[] inputs)
        {
            this.inputs = inputs;

            for (int i = 0; i < numberOfOutputs; i++)
            {
                this.outputs[i] = 0;
                for (int j = 0; j < numberOfInputs; j++)
                {
                    this.outputs[i] += this.inputs[j] * this.weights[i, j];
                }

                outputs[i] = this.TanH(outputs[i]);
            }

            return this.outputs;
        }

        public void InitRandomWeights()
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] = (float)rand.NextDouble() - 0.5f;
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

        public void BackwardPropagationOutput(float[] expected)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                error[i] = outputs[i] - expected[i];
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                gamma[i] = error[i] * TanHDerivative(outputs[i]);
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }
        }

        public void BackwardPropagationHidden(float[] gammaForward, float[,] weightsForward)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                gamma[i] = 0;

                for (int j = 0; j < gammaForward.Length; j++)
                {
                    gamma[i] += gamma[j] * weightsForward[j, i];
                }

                gamma[i] *= this.TanHDerivative(outputs[i]);
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }
        }

        public float TanH(float value)
        {
            return (float)Math.Tanh(value);
        }

        public float TanHDerivative(float value)
        {
            return 1 - (value * value);
        }
    }
}
