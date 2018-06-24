namespace Network.Factories
{
    using Network.Interfaces;
    using Network.Layers;
    using Network.Neurons;

    /// <summary>
    /// Factory used to create layers.
    /// </summary>
    public class NeuralLayerFactory
    {
        public NeuralLayer CreateNeuralLayer(int numberOfNeurons, IActivationFunction activationFunction)
        {
            var layer = new NeuralLayer();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                var neuron = new Neuron(activationFunction);
                layer.Neurons.Add(neuron);
            }

            return layer;
        }
    }
}
