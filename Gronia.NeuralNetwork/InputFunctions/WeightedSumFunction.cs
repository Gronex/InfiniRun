using System;
using System.Collections.Generic;
using System.Linq;

namespace Gronia.NeuralNetwork.InputFunctions
{
    [Serializable]
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(IEnumerable<ISynapse> inputs)
        {
            return inputs.Sum(x => x.Weight * x.GetOutput());
        }
    }
}
