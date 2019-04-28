using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Gronia.NeuralNetwork.ActivationFunctions;
using Gronia.NeuralNetwork.InputFunctions;
using Gronia.NeuralNetwork.Synapses;

namespace Gronia.NeuralNetwork
{
    [Serializable]
    public class NeuralNetwork
    {
        private readonly List<NeuralLayer> _layers;

        private NeuralLayer InputLayer => _layers.First();

        private NeuralLayer OutputLayer => _layers.Last();

        public static NeuralNetwork GeneticNeuralNetwork(int inputNeurons, MutateSynapse mutater)
        {
            return new NeuralNetwork(inputNeurons, (activationFunction, inputFunction) => new Neuron(activationFunction, inputFunction), x => new Synapse(x, mutater), x => new InputSynapse(x));
        }

        public NeuralNetwork(int inputNeurons, NeuronFactory neuronFactory, SynapseFactory synapseFactory, InputSynapseFactory inputSynapseFactory)
        {
            if (inputNeurons < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(inputNeurons), "Must be greater than 0");
            }

            _layers = new List<NeuralLayer>();
            AddLayer(inputNeurons, new RectifiedActivationFunction(), new WeightedSumFunction(), neuronFactory, synapseFactory);

            foreach(INeuron neuron in InputLayer.Neurons)
            {
                neuron.AddInputSynapse(0, inputSynapseFactory);
            }
        }

        public NeuralNetwork AddLayer(int neuronCount, IActivationFunction activationFunction, IInputFunction inputFunction, MutateSynapse mutater)
        {
            return AddLayer(neuronCount, activationFunction, inputFunction, (a, i) => new Neuron(a, i), x => new Synapse(x, mutater));
        }

        public NeuralNetwork AddLayer(int neuronCount, IActivationFunction activationFunction, IInputFunction inputFunction, NeuronFactory neuronFactory, SynapseFactory synapseFactory)
        {
            if (neuronCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(neuronCount), "Must be greater than 0");
            }

            var layer = new NeuralLayer();
            for (int i = 0; i < neuronCount; i++)
            {
                layer.Neurons.Add(neuronFactory(activationFunction, inputFunction));
            }

            if (_layers.Any())
            {
                layer.ConnectLayer(_layers.Last(), synapseFactory);
            }
            _layers.Add(layer);
            return this;
        }

        public NeuralNetwork Clone()
        {
            using(var stream = new MemoryStream())
            {
                if (GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream) as NeuralNetwork;
                }
                return null;
            }
        }

        public void SetInputs(params double[] inputs)
        {
            INeuron[] neurons = InputLayer.Neurons.ToArray();

            if (neurons.Count() != inputs.Length)
            {
                throw new ArgumentException($"There are {neurons.Count()} neurons, but {inputs.Length} inputs. They have to match", nameof(inputs));
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[i].PushValueOnInput(inputs[i]);
            }
        }

        public IEnumerable<double> GetOutput(params double[] inputs)
        {
            SetInputs(inputs);
            return GetOutput();
        }

        public IEnumerable<double> GetOutput()
        {
            foreach (INeuron neuron in OutputLayer.Neurons)
            {
                yield return neuron.CalculateOutput();
            }
        }

        public void MutateNetwork(MutateSynapse mutationFunction)
        {
            foreach (IMutatableSynapse connection in _layers.Skip(1).SelectMany(x => x.Neurons).SelectMany(x => x.Inputs).OfType<IMutatableSynapse>())
            {
                connection.Mutate(mutationFunction);
            }
        }
    }

    public delegate INeuron NeuronFactory(IActivationFunction activationFunction, IInputFunction inputFunction);
}
