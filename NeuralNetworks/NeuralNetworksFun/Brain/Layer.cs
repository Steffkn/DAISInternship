using System;
using System.Collections.Generic;
using System.Text;

namespace Brain
{
    /// <summary>
    /// Each individual layer in the ML{
    /// </summary>
    public class Layer
    {
        private const float LearningRate = 0.033f;
        private const float BIAS = 1.0f;
        int numberOfInputs; //# of neurons in the previous layer
        int numberOfOuputs; //# of neurons in the current layer

        public float[] outputs; //outputs of this layer
        public float[] inputs; //inputs in into this layer
        public float[,] weights; //weights of this layer
        public float[,] weightsDelta; //deltas of this layer
        public float[] gamma; //gamma of this layer
        public float[] error; //error of the output layer

        public static Random random = new Random(); //Static random class variable

        /// <summary>
        /// Constructor initilizes vaiour data structures
        /// </summary>
        /// <param name="numberOfInputs">Number of neurons in the previous layer</param>
        /// <param name="numberOfOuputs">Number of neurons in the current layer</param>
        public Layer(int numberOfInputs, int numberOfOuputs)
        {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOuputs = numberOfOuputs;

            //initilize datastructures
            outputs = new float[numberOfOuputs];
            inputs = new float[numberOfInputs];
            weights = new float[numberOfOuputs, numberOfInputs];
            weightsDelta = new float[numberOfOuputs, numberOfInputs];
            gamma = new float[numberOfOuputs];
            error = new float[numberOfOuputs];

            InitilizeWeights(); //initilize weights
        }

        /// <summary>
        /// Constructor initilizes vaiour data structures
        /// </summary>
        /// <param name="numberOfInputs">Number of neurons in the previous layer</param>
        /// <param name="numberOfOuputs">Number of neurons in the current layer</param>
        public Layer(float[,] weights)
        {
            this.numberOfInputs = weights.GetLength(1);
            this.numberOfOuputs = weights.GetLength(0);

            //initilize datastructures
            outputs = new float[this.numberOfOuputs];
            inputs = new float[this.numberOfInputs];
            this.weights = weights;
            weightsDelta = new float[this.numberOfOuputs, this.numberOfInputs];
            gamma = new float[this.numberOfOuputs];
            error = new float[this.numberOfOuputs];
        }
        /// <summary>
        /// Initilize weights between -0.5 and 0.5
        /// </summary>
        public void InitilizeWeights()
        {
            for (int i = 0; i < numberOfOuputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] = (float)random.NextDouble() - 0.5f;
                }
            }
        }

        /// <summary>
        /// Feedforward this layer with a given input
        /// </summary>
        /// <param name="inputs">The output values of the previous layer</param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputs)
        {
            this.inputs = inputs;// keep shallow copy which can be used for back propagation

            //feed forwards
            for (int i = 0; i < numberOfOuputs; i++)
            {
                outputs[i] = 0;
                int j = 0;
                for (; j < numberOfInputs - 1; j++)
                {
                    outputs[i] += inputs[j] * weights[i, j];
                }

                // BIAS neuron
                outputs[i] += BIAS * weights[i, j];

                outputs[i] = (float)Math.Tanh(outputs[i]);
            }

            return outputs;
        }

        /// <summary>
        /// TanH derivate 
        /// </summary>
        /// <param name="value">An already computed TanH value</param>
        /// <returns></returns>
        public float TanHDer(float value)
        {
            return 1 - (value * value);
        }

        /// <summary>
        /// Back propagation for the output layer
        /// </summary>
        /// <param name="expected">The expected output</param>
        public void BackPropOutput(float[] expected)
        {
            //Error dervative of the cost function
            for (int i = 0; i < numberOfOuputs; i++)
            {
                error[i] = outputs[i] - expected[i];
            }

            //Gamma calculation
            for (int i = 0; i < numberOfOuputs; i++)
            {
                gamma[i] = error[i] * TanHDer(outputs[i]);
            }

            //Caluclating detla weights
            for (int i = 0; i < numberOfOuputs; i++)
            {
                int j = 0;
                for (; j < numberOfInputs - 1; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }

                // BIAS neuron
                weightsDelta[i, j] = gamma[i] * BIAS;
            }
        }

        /// <summary>
        /// Back propagation for the hidden layers
        /// </summary>
        /// <param name="gammaForward">the gamma value of the forward layer</param>
        /// <param name="weightsFoward">the weights of the forward layer</param>
        public void BackPropHidden(float[] gammaForward, float[,] weightsFoward)
        {
            //Caluclate new gamma using gamma sums of the forward layer
            for (int i = 0; i < numberOfOuputs; i++)
            {
                gamma[i] = 0;

                for (int j = 0; j < gammaForward.Length; j++)
                {
                    gamma[i] += gammaForward[j] * weightsFoward[j, i];
                }

                gamma[i] *= TanHDer(outputs[i]);
            }

            //Caluclating detla weights
            for (int i = 0; i < numberOfOuputs; i++)
            {
                int j = 0;
                for (; j < numberOfInputs-1; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }

                weightsDelta[i, j] = gamma[i] * BIAS;
            }
        }

        /// <summary>
        /// Updating weights
        /// </summary>
        public void UpdateWeights()
        {
            for (int i = 0; i < numberOfOuputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] -= weightsDelta[i, j] * LearningRate;
                }
            }
        }
    }
}
