namespace CustomNetwork.Interfaces
{
    using System;
    using System.Collections.Generic;
    using CustomNetwork.Common;

    public interface INeuron
    {
        Guid Id { get; }

        Content Content { get; }

        List<ISynapse> Inputs { get; }

        double PreviousPartialDerivate { get; }

        double CalculateValue(Func<double, double> activationFunction);
    }
}
