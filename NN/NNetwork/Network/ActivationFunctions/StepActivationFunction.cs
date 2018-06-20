namespace Network.ActivationFunction
{
    using System;
    using Network.Interfaces;

    public class StepActivationFunction : IActivationFunction
    {
        private double treshold;

        public StepActivationFunction(double treshold)
        {
            this.treshold = treshold;
        }

        public double CalculateOutput(double input)
        {
            return Convert.ToDouble(input > this.treshold);
        }
    }
}
