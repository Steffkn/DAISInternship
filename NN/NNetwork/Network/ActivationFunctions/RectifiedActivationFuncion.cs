namespace Network.ActivationFunctions
{
    using System;
    using Network.Interfaces;

    public class RectifiedActivationFuncion : IActivationFunction
    {
        public double CalculateOutput(double input)
        {
            return Math.Max(0, input);
        }
    }
}
