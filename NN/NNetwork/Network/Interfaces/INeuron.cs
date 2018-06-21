using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Interfaces
{
    public interface INeuron
    {
        Guid Id { get; }

        double PreviousPartialDerivate { get; set; }

        List<ISynapse> Inputs { get; set; }

        List<ISynapse> Outputs { get; set; }

        void AddInputNeuron(INeuron inputNeuron);

        void AddOutputNeuron(INeuron outputNeuron);

        double CalculateOutput();

        void AddInputSynapse(double inputValue);

        void PushValueOnInput(double inputValue);
    }
}
