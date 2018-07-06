using Nuts.TransferFunctions;

namespace Nuts
{
    public class NeuralNetwork
    {
        private int[] layer;
        private Layer[] layers;
        private ITransferFunction transferFunction;

        public NeuralNetwork(int[] layer, ITransferFunction transferFunction)
        {
            this.layer = new int[layer.Length];
            this.transferFunction = transferFunction;

            for (int i = 0; i < layer.Length; i++)
            {
                this.layer[i] = layer[i];
            }

            // ignore input layer
            this.layers = new Layer[layer.Length - 1];

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(layer[i], layer[i + 1]);
            }
        }

        public double[] FeedForward(double[] inputs)
        {
            layers[0].FeedForward(inputs, transferFunction);

            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].FeedForward(layers[i - 1].outputs, transferFunction);
            }

            return layers[layers.Length - 1].outputs;
        }

        public void BackwardPropagation(double[] expected)
        {
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                if (i == layers.Length - 1)
                {
                    layers[i].BackwardPropagationOutput(expected, transferFunction);
                }
                else
                {
                    layers[i].BackwardPropagationHidden(layers[i + 1].gamma, layers[i + 1].weights, transferFunction);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights();
            }
        }
    }
}
