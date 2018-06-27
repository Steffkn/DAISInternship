namespace MindLib.TransferFunctions
{
    using System;

    public class SigmoidTransferFunction : ITransferFunction
    {
        public double Activate(double value)
        {
            return 1 / (1 + Math.Exp(-1 * value));
        }

        public double Derivative(double value)
        {
            return value * (1 - value);
        }
    }
}
