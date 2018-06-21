namespace Network.Interfaces
{
    using System;
    using System.Collections.Generic;

    [Obsolete]
    public interface IInputFunction
    {
        double CalculateInput(List<ISynapse> inputs);
    }
}
