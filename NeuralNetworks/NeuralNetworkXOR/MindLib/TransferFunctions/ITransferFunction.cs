namespace MindLib.TransferFunctions
{
    public interface ITransferFunction
    {
        double Activate(double value);

        double Derivative(double value);
    }
}
