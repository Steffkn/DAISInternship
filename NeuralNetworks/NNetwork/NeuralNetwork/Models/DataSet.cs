namespace NeuralNetwork.Models
{
    public class DataSet
    {
        public double[] Values { get; set; }
        public double[] Targets { get; set; }

        public DataSet(double[] values, double[] targets)
        {
            this.Values = values;
            this.Targets = targets;
        }
    }
}
