using System;
using System.Collections.Generic;
using System.Linq;
using Gronia.NeuralNetwork.ActivationFunctions;
using Gronia.NeuralNetwork.InputFunctions;

namespace Gronia.NeuralNetwork
{
    [Serializable]
    public class Neuron : INeuron
    {
        private readonly IActivationFunction _activationFunction;
        private readonly IInputFunction _inputFunction;

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            _activationFunction = activationFunction;
            _inputFunction = inputFunction;
            Id = Guid.NewGuid();
            Inputs = new List<ISynapse>();
            Outputs = new List<ISynapse>();
        }

        public Guid Id { get; }

        public ICollection<ISynapse> Inputs { get; }

        public ICollection<ISynapse> Outputs { get; }

        public void AddInputNeuron(INeuron neuron, SynapseFactory synapseFactory)
        {
            ISynapse synapse = synapseFactory(neuron);
            Inputs.Add(synapse);
            neuron.Outputs.Add(synapse);
        }

        public void AddInputSynapse(double value, InputSynapseFactory inputSynapseFactory)
        {
            IInputSynapse inputSynapse = inputSynapseFactory(value);
            Inputs.Add(inputSynapse);
        }

        public double CalculateOutput()
        {
            var input = _inputFunction.CalculateInput(Inputs);
            return _activationFunction.CalculateOutput(input);
        }

        public void PushValueOnInput(double value)
        {
            Inputs.OfType<IInputSynapse>().First().Output = value;
        }
    }
}
