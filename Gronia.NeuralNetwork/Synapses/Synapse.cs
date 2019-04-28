using System;
using Gronia.NeuralNetwork.Synapses;

namespace Gronia.NeuralNetwork
{
    [Serializable]
    internal class Synapse : IMutatableSynapse
    {
        private readonly INeuron _inputNeuron;

        public Synapse(INeuron neuron, MutateSynapse mutator)
        {
            _inputNeuron = neuron;
            Weight = mutator(null);
        }

        public double Weight { get; private set; }


        public double GetOutput()
        {
            return _inputNeuron.CalculateOutput();
        }

        public bool IsFromNeuron(Guid neuronId)
        {
            return _inputNeuron.Id == neuronId;
        }

        public void Mutate(MutateSynapse mutator)
        {
            Weight = mutator(Weight);
        }
    }
}
