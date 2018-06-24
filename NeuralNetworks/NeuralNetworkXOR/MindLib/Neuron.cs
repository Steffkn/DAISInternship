using System;
using System.Collections.Generic;

namespace MindLib
{
    public class Neuron
    {
        private double netSum = 0;
        private double neuronSum = 0;
        private double totalErrors = 0;
        private double deltaErrors = 0;

        public double NetSum { get => netSum; set => netSum = value; }
        public double NeuronSum { get => neuronSum; set => neuronSum = value; }
        public double TotalErrors { get => totalErrors; set => totalErrors = value; }
        public double DeltaErrors { get => deltaErrors; set => deltaErrors = value; }
    }
}
