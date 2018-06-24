namespace Network.Synapses
{
    using System;
    using Network.Interfaces;

    public class InputSynapse : ISynapse
    {
        internal INeuron _toNeuron;

        public double Weight { get; set; }

        public double Output { get; set; }

        public double PreviousWeight { get; set; }

        public InputSynapse(INeuron toNeuron)
        {
            this._toNeuron = toNeuron;
            this.Weight = 1;
        }

        public InputSynapse(INeuron toNeuron, double output)
        {
            this._toNeuron = toNeuron;
            this.Output = output;
            this.Weight = 1;
            this.PreviousWeight = 1;
        }

        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return false;
        }

        public void UpdateWeight(double learningRate, double delta)
        {
            throw new InvalidOperationException("It is not allowed to call this method on Input Connection");
        }
    }
}
