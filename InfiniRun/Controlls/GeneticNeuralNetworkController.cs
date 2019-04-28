using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gronia.NeuralNetwork;
using Gronia.NeuralNetwork.ActivationFunctions;
using Gronia.NeuralNetwork.InputFunctions;
using InfiniRun.GameObjects;
using InfiniRun.Managers;

namespace InfiniRun.Controlls
{
    public class GeneticNeuralNetworkController : IInputController
    {
        private readonly Random _random;
        private readonly NeuralNetwork _brain;

        public Character Character { get; set; }

        public GeneticNeuralNetworkController()
        {
            _random = new Random();

            _brain = NeuralNetwork.GeneticNeuralNetwork(9, Mutate);

            _brain
                .AddLayer(10, new SigmoidActivationFunction(1), new WeightedSumFunction(), Mutate)
                .AddLayer(5, new SigmoidActivationFunction(5), new WeightedSumFunction(), Mutate)
                .AddLayer(1, new StepActivationFunction(5), new WeightedSumFunction(), Mutate);
        }

        public GeneticNeuralNetworkController(GeneticNeuralNetworkController controller)
        {
            _brain = controller._brain.Clone();
            _random = new Random();
        }

        public Command GetCommand(Character actor, EnvironmentContext environment)
        {
            Obstacle[] obstacles = environment.Obstacles
                .Where(x => actor.Position.X < x.Position.X)
                .OrderBy(x => x.Position.X)
                .Take(3)
                .ToArray();

            var inputs = new double[]
            {
                actor.Position.X,
                actor.Position.Y,
                environment.Ground.Position.Y,
                obstacles.Length > 0 ? obstacles[0].Position.X : -1,
                obstacles.Length > 0 ? obstacles[0].Position.Y : -1,
                obstacles.Length > 1 ? obstacles[1].Position.X : -1,
                obstacles.Length > 1 ? obstacles[1].Position.Y : -1,
                obstacles.Length > 2 ? obstacles[2].Position.X : -1,
                obstacles.Length > 2 ? obstacles[2].Position.Y : -1,
            };

            var outputs = _brain.GetOutput(inputs).ToArray();

            if(outputs[0] > 0)
            {
                return Command.Jump;
            }
            else
            {
                return Command.None;
            }
        }

        public double CalculateFitness()
        {
            return Character.Score * Character.Score;
        }

        public void MutateNetwork()
        {
            _brain.MutateNetwork(Mutate);
        }

        private double Mutate(double? weight)
        {
            if (!weight.HasValue)
            {
                return _random.NextDouble() * 10 - 5;
            }
            if(_random.Next(0, 99) < 10)
            {
                return weight.Value + _random.NextDouble() * 2 - 1;
            }
            return weight.Value;
        }

        public GeneticNeuralNetworkController Clone()
        {
            return new GeneticNeuralNetworkController(this);
        }
    }
}
