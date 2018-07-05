namespace Nuts
{
    public class NeuralNetwork
    {
        int[] layer;
        Layer[] layers;

        public NeuralNetwork(int[] layer)
        {
            this.layer = new int[layer.Length];

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

        public float[] FeedForward(float[] inputs)
        {
            layers[0].FeedForward(inputs);

            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].FeedForward(layers[i - 1].outputs);
            }

            return layers[layers.Length - 1].outputs;
        }

        public void BackwardPropagation(float[] expected)
        {
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                if (i == layers.Length - 1)
                {
                    layers[i].BackwardPropagationOutput(expected);
                }
                else
                {
                    layers[i].BackwardPropagationHidden(layers[i + 1].gamma, layers[i + 1].weights);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights();
            }
        }
    }
}
