namespace MindLib.TransferFunctions
{
    using System;

    public class ReLUTransferFunction : ITransferFunction
    {
        public double Activate(double value)
        {
            return Math.Max(0.0, value);
        }

        public double Derivative(double value)
        {
            return Math.Max(0.0, value);
        }
    }
}
