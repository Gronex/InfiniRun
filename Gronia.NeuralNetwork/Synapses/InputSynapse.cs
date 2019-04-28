using System;

namespace Gronia.NeuralNetwork
{
    [Serializable]
    public class InputSynapse : IInputSynapse
    {
        public InputSynapse(double output)
        {
            Output = output;
        }

        public double Weight => 1;

        public double Output { get; set; }

        public double GetOutput()
        {
            return Output;
        }

        public bool IsFromNeuron(Guid neuronId)
        {
            return false;
        }
    }
}
