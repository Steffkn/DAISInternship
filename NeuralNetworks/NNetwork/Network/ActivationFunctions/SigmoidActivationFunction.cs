namespace Network.ActivationFunctions
{
    using System;
    using Network.Interfaces;

    /// <summary>
    /// Implementation of Sigmoid Activation Function.
    /// </summary>
    public class SigmoidActivationFunction : IActivationFunction
    {
        private double coeficient;

        public SigmoidActivationFunction(double coeficient)
        {
            this.coeficient = coeficient;
        }

        public double CalculateOutput(double input)
        {
            return (1 / (1 + Math.Exp(-input * this.coeficient)));
        }
    }
}
