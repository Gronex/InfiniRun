using System;
using System.Collections.Generic;
using System.Linq;

namespace Gronia.NeuralNetwork
{
    [Serializable]
    public class NeuralLayer
    {
        public ICollection<INeuron> Neurons { get; private set; }

        public NeuralLayer()
        {
            Neurons = new List<INeuron>();
        }

        public void ConnectLayer(NeuralLayer layer, SynapseFactory synapseFactory)
        {
            foreach (INeuron neuron in Neurons)
            {
                foreach (INeuron inputNeuron in layer.Neurons)
                {
                    neuron.AddInputNeuron(inputNeuron, synapseFactory);
                }
            }
        }
    }
}
