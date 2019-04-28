using System;
using System.Collections.Generic;
using System.Text;

namespace Gronia.NeuralNetwork.Synapses
{
    public interface IMutatableSynapse : ISynapse
    {
        void Mutate(MutateSynapse mutator);
    }

    /// <summary>
    /// Calculate the new weight of the synapse based on the previous one
    /// </summary>
    /// <param name="previousWeight">the previous weight, or null if fresh.</param>
    /// <returns>The new weight the synapse will use.</returns>
    [Serializable]
    public delegate double MutateSynapse(double? previousWeight);
}
