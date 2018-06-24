using System;

namespace Brain
{
    /// <summary>
    /// Each individual layer in the ML
    /// </summary>
    public class Layer
    {
        private const float LearningRate = 0.033f;
        private const float BIAS = 1.0f;
        int numberOfInputs; //# of neurons in the previous layer
        int numberOfOuputs; //# of neurons in the current layer

        private float[] outputs; //outputs of this layer
        private float[] inputs; //inputs in into this layer
        private float[,] weights; //weights of this layer
        private float[,] weightsDelta; //deltas of this layer
        private float[] gamma; //gamma of this layer
        private float[] error; //error of the output layer

        private static Random random = new Random(); //Static random class variable

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
            Outputs = new float[numberOfOuputs];
            inputs = new float[numberOfInputs];
            Weights = new float[numberOfOuputs, numberOfInputs];
            weightsDelta = new float[numberOfOuputs, numberOfInputs];
            Gamma = new float[numberOfOuputs];
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
            this.Outputs = new float[this.numberOfOuputs];
            inputs = new float[this.numberOfInputs];
            this.Weights = weights;
            weightsDelta = new float[this.numberOfOuputs, this.numberOfInputs];
            Gamma = new float[this.numberOfOuputs];
            error = new float[this.numberOfOuputs];
        }

        public float[] Outputs
        {
            get { return outputs; }
            set { outputs = value; }
        }

        public float[,] Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        public float[] Gamma
        {
            get { return gamma; }
            set { gamma = value; }
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
                    Weights[i, j] = (float)random.NextDouble() - 0.5f;
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
            this.inputs = inputs; // keep shallow copy which can be used for back propagation

            //feed forwards
            for (int i = 0; i < numberOfOuputs; i++)
            {
                Outputs[i] = 0;
                int j = 0;
                for (; j < numberOfInputs - 1; j++)
                {
                    Outputs[i] += inputs[j] * Weights[i, j];
                }

                // BIAS neuron
                Outputs[i] += BIAS * Weights[i, j];

                Outputs[i] = (float)Math.Tanh(Outputs[i]);
            }

            return Outputs;
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
            //Error derivative of the cost function
            for (int i = 0; i < numberOfOuputs; i++)
            {
                error[i] = Outputs[i] - expected[i];
            }

            //Gamma calculation
            for (int i = 0; i < numberOfOuputs; i++)
            {
                Gamma[i] = error[i] * TanHDer(Outputs[i]);
            }

            //Caluclating detla weights
            for (int i = 0; i < numberOfOuputs; i++)
            {
                int j = 0;
                for (; j < numberOfInputs - 1; j++)
                {
                    weightsDelta[i, j] = Gamma[i] * inputs[j];
                }

                // BIAS neuron
                weightsDelta[i, j] = Gamma[i] * BIAS;
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
                Gamma[i] = 0;

                for (int j = 0; j < gammaForward.Length; j++)
                {
                    Gamma[i] += gammaForward[j] * weightsFoward[j, i];
                }

                Gamma[i] *= TanHDer(Outputs[i]);
            }

            //Caluclating detla weights
            for (int i = 0; i < numberOfOuputs; i++)
            {
                int j = 0;
                for (; j < numberOfInputs - 1; j++)
                {
                    weightsDelta[i, j] = Gamma[i] * inputs[j];
                }

                weightsDelta[i, j] = Gamma[i] * BIAS;
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
                    Weights[i, j] -= weightsDelta[i, j] * LearningRate;
                }
            }
        }
    }
}
