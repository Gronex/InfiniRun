using System;
using System.Collections.Generic;
using System.Text;

namespace Gronia.NeuralNetwork.InputFunctions
{
    public interface IInputFunction
    {
        double CalculateInput(IEnumerable<ISynapse> inputs);
    }
}
