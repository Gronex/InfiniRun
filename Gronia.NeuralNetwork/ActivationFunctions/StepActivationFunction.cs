using System;
using System.Collections.Generic;
using System.Text;

namespace Gronia.NeuralNetwork.ActivationFunctions
{
    [Serializable]
    public class StepActivationFunction : IActivationFunction
    {
        private readonly double _threshold;

        public StepActivationFunction(double threshold)
        {
            _threshold = threshold;
        }

        public double CalculateOutput(double input)
        {
            return Convert.ToDouble(input > _threshold);
        }
    }
}
