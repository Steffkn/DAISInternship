namespace MindLib
{
    public class Synapse
    {
        public Synapse(Content inputContent, Content error, double initialWeight)
        {
            this.InputNeuronContent = inputContent;
            this.DError = error;
            this.Weight = initialWeight;
        }

        public Content InputNeuronContent { get; }

        public Content DError { get; set; }

        public double Weight { get; set; }

        public double DeltaWeight { get; set; }

        public double Get()
        {
            return this.InputNeuronContent.Value * this.Weight;
        }

        public void CorrectWeights(double learningRage)
        {
            this.DeltaWeight = -1 * learningRage * DError.Value * InputNeuronContent.Value;
            this.Weight += this.DeltaWeight;
        }
    }
}
