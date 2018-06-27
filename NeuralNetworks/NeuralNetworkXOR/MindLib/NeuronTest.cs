using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindLib
{
    public class NeuronTest
    {
        public NeuronTest(int id)
        {
            this.Id = id;
            this.Inputs = new List<Synapse>();
            this.NeuronSum = new Content();
            this.DError = new Content();
        }

        public int Id { get; private set; }

        public double NetSum { get; set; }

        public Content NeuronSum { get; set; }

        public List<Synapse> Inputs { get; private set; }

        public Content DError { get; set; }

        public double Error { get; set; }

        public double DeltaError { get; set; }

        public double CalculateValue(Func<double, double> activationFunction)
        {
            this.NetSum = 0;
            foreach (var synapse in this.Inputs)
            {
                this.NetSum += synapse.Get();
            }

            this.NeuronSum.Value = activationFunction.Invoke(this.NetSum);
            return this.NeuronSum.Value;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(this.Id.ToString());
            stringBuilder.AppendLine($"\tValue: \t{this.NeuronSum.Value}");
            stringBuilder.AppendFormat("\tw: \t{0}", string.Join("\t", this.Inputs.Select(s => s.Weight)));
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }
    }
}
