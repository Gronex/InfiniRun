using System;
using System.Collections.Generic;

namespace Gronia.NeuralNetwork
{
    public interface INeuron
    {
        Guid Id { get; }

        ICollection<ISynapse> Inputs { get; }

        ICollection<ISynapse> Outputs { get; }

        void AddInputNeuron(INeuron neuron, SynapseFactory synapseFactory);

        void AddInputSynapse(double value, InputSynapseFactory inputSynapseFactory);

        double CalculateOutput();

        void PushValueOnInput(double value);
    }


    public delegate ISynapse SynapseFactory(INeuron neuron);
    public delegate IInputSynapse InputSynapseFactory(double inputValue);
}
