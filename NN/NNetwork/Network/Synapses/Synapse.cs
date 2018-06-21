namespace Network.Synapses
{
    using System;
    using Network.Interfaces;

    public class Synapse : ISynapse
    {
        internal INeuron _fromNeuron;
        internal INeuron _toNeuron;

        public Synapse(INeuron fromNeuraon, INeuron toNeuron, double weight)
        {
            this._fromNeuron = fromNeuraon;
            this._toNeuron = toNeuron;

            this.Weight = weight;
            this.PreviousWeight = 0;
        }

        public Synapse(INeuron fromNeuraon, INeuron toNeuron)
        {
            this._fromNeuron = fromNeuraon;
            this._toNeuron = toNeuron;

            var tmpRandom = new Random();
            this.Weight = tmpRandom.NextDouble();
            this.PreviousWeight = 0;
        }

        /// <summary>
        /// Weight of the connection.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Get output value of the connection.
        /// </summary>
        /// <returns>
        /// Output value of the connection.
        /// </returns>
        public double Output { get { return this._fromNeuron.CalculateOutput(); } }

        /// <summary>
        /// Weight that connection had in previous itteration.
        /// Used in training process.
        /// </summary>
        public double PreviousWeight { get; set; }

        /// <summary>
        /// Checks if Neuron has a certain number as an input neuron.
        /// </summary>
        /// <param name="fromNeuronId">Neuron Id.</param>
        /// <returns>
        /// True - if the neuron is the input of the connection.
        /// False - if the neuron is not the input of the connection. 
        /// </returns>
        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return this._fromNeuron.Id.Equals(fromNeuronId);
        }

        /// <summary>
        /// Update weight.
        /// </summary>
        /// <param name="learningRate">Chosen learning rate.</param>
        /// <param name="delta">Calculated difference for which weight 
        /// of the connection needs to be modified.</param>
        public void UpdateWeight(double learningRate, double delta)
        {
            this.PreviousWeight = Weight;
            this.Weight += learningRate * delta;
        }
    }
}
