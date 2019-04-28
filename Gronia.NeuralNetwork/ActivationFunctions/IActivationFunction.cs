using System;
using System.Collections.Generic;
using System.Text;

namespace Gronia.NeuralNetwork.ActivationFunctions
{
    public interface IActivationFunction
    {
        double CalculateOutput(double input);
    }
}
