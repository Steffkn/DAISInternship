namespace Brain
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Simple MLP Neural Network
    /// </summary>
    public class NeuralNetwork
    {
        private const string WeightsFilePostfix = "_weights";
        private const string OutputsFilePostfix = "_out";
        private const string InputFilePostfix = "_in";
        private const string SavePathFormat = "..\\Memory\\";
        int[] layer; //layer information
        Layer[] layers; //layers in the network

        /// <summary>
        /// Constructor setting up layers
        /// </summary>
        /// <param name="layer">Layers of this network</param>
        public NeuralNetwork(int[] layer, bool isNew)
        {
            //deep copy layers
            this.layer = new int[layer.Length];
            for (int i = 0; i < layer.Length; i++)
            {
                this.layer[i] = layer[i];
            }

            //creates neural layers
            this.layers = new Layer[layer.Length - 1];

            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = new Layer(layer[i] + 1, layer[i + 1]);
            }
        }

        public NeuralNetwork(string fileName)
        {
            string path = SavePathFormat;

            float[][,] weights = MemoryHelper.ReadFromFile(path + fileName + WeightsFilePostfix);

            //deep copy layers
            this.layer = new int[weights.GetLength(0) + 1];
            for (int i = 0; i < layer.Length; i++)
            {
                this.layer[i] = layer[i];
            }

            //creates neural layers
            this.layers = new Layer[layer.Length - 1];

            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = new Layer(weights[i]);
            }
        }

        /// <summary>
        /// High level feedforward for this network
        /// </summary>
        /// <param name="inputs">Inputs to be feed forwared</param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputs)
        {
            //feed forward
            layers[0].FeedForward(inputs);
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].FeedForward(layers[i - 1].outputs);
            }

            return layers[layers.Length - 1].outputs; //return output of last layer
        }

        /// <summary>
        /// High level back porpagation
        /// Note: It is expexted the one feed forward was done before this back prop.
        /// </summary>
        /// <param name="expected">The expected output form the last feedforward</param>
        public void BackProp(float[] expected)
        {
            // run over all layers backwards
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                if (i == layers.Length - 1)
                {
                    layers[i].BackPropOutput(expected); //back prop output
                }
                else
                {
                    layers[i].BackPropHidden(layers[i + 1].gamma, layers[i + 1].weights); //back prop hidden
                }
            }

            //Update weights
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights();
            }
        }

        public void Save(string fileName, float[,] input, float[,] output)
        {
            float[,] weights = new float[layers[0].weights.GetLength(0) * layers.Length, layers[0].weights.GetLength(1) * layers.Length];
            string path = SavePathFormat;

            MemoryHelper.WriteToFile(path + fileName + InputFilePostfix, input, FileMode.Create);
            MemoryHelper.WriteToFile(path + fileName + OutputsFilePostfix, output, FileMode.Create);
            MemoryHelper.WriteToFile(path + fileName + WeightsFilePostfix, layers[0].weights, FileMode.Create);

            for (int i = 1; i < layers.Length; i++)
            {
                MemoryHelper.WriteToFile(path + fileName + WeightsFilePostfix, layers[i].weights, FileMode.Append);
            }
        }

        public void Learn(float[,] inputs, float[,] outputs, int iterations = 10000)
        {
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < inputs.GetLength(0); j++)
                {
                    FeedForward(ArrayHelper.GetRow(inputs, j));
                    BackProp(ArrayHelper.GetRow(outputs, j));
                }
            }

            Console.WriteLine("Save result? y/n");
            string response = Console.ReadLine();
            if (response.ToLower()[0] == 'y')
            {

                Console.Write("Operation name: ");
                string name = Console.ReadLine();

                Save(name, inputs, outputs);
            }
        }
    }
}