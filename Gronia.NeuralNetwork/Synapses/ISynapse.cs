using System;

namespace Gronia.NeuralNetwork
{
    public interface ISynapse
    {
        double Weight { get; }

        double GetOutput();

        bool IsFromNeuron(Guid neuronId);
    }
}
