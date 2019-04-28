using System;
using System.Collections.Generic;
using System.Text;

namespace Gronia.NeuralNetwork.ActivationFunctions
{
    [Serializable]
    public class RectifiedActivationFunction : IActivationFunction
    {
        public double CalculateOutput(double input)
        {
            return Math.Max(0, input);
        }
    }
}
