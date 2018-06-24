namespace Network.Layers
{
    using System.Collections.Generic;
    using System.Linq;
    using Network.Interfaces;

    public class NeuralLayer
    {
        public List<INeuron> Neurons;

        public NeuralLayer()
        {
            this.Neurons = new List<INeuron>();
        }

        /// <summary>
        /// Connecting two layers.
        /// </summary>
        public void ConnectLayers(NeuralLayer inputLayer)
        {
            var combos = this.Neurons.SelectMany(neuron => inputLayer.Neurons, (neuron, input) => new { neuron, input });

            foreach (var combo in combos.ToList())
            {
                combo.neuron.AddInputNeuron(combo.input);
            }
        }
    }
}
