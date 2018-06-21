namespace CustomNetwork
{
    using System;
    using System.Collections.Generic;
    using CustomNetwork.Interfaces;

    public class NNetwork
    {
        private int numberOfInputData;
        private int numberOfOutputData;
        private int numberOfHidenLayers;
        private LinkedList<ILayer> layers;

        public NNetwork(int numberOfInputData, int numberOfOutputData, int numberOfHidenLayers = 3, int numberOfNeuronsInHidenLayers = 10)
        {
            this.numberOfInputData = numberOfInputData;
            this.numberOfOutputData = numberOfOutputData;
            this.numberOfHidenLayers = numberOfHidenLayers;
            this.layers = new LinkedList<ILayer>();

            var inputLayer = new NeuralLayer(numberOfInputData);
            var outputLayer = new NeuralLayer(numberOfOutputData);

            this.layers.AddFirst(inputLayer);


            for (int i = 0; i < numberOfHidenLayers; i++)
            {
                this.layers.AddLast(new NeuralLayer(numberOfNeuronsInHidenLayers));
            }

            this.layers.AddLast(outputLayer);

            var currentLayer = this.layers.First;
            while (currentLayer != null && currentLayer.Next != null)
            {
                currentLayer = currentLayer.Next;
                foreach (var neuron in currentLayer.Value.Neurons)
                {
                    foreach (var prevLayerNeuron in currentLayer.Previous.Value.Neurons)
                    {
                        neuron.Inputs.Add(new Synapse(prevLayerNeuron));
                    }
                }
            }
        }
    }
}
