namespace Nuts.TransferFunctions
{
    using System;

    public class TanHTransferFunction : ITransferFunction
    {
        public double Activate(double value)
        {
            return (2 / (1 + Math.Exp(-2 * value))) - 1;
        }

        public double Derivative(double value)
        {
            return 1 - (value * value);
        }
    }
}
