using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniRun.Controlls;
using InfiniRun.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniRun.Managers
{
    public class PopulationManager
    {
        private readonly Random _random = new Random();
        private EnvironmentContext _environmentContext;
        private int _generation;
        private Texture2D _groundTexture;
        private Texture2D _characterTexture;
        private Texture2D _obstacleTexture;
        private double _highScore;

        private readonly List<GeneticNeuralNetworkController> _players;

        private const int PlayerCount = 100;

        public PopulationManager()
        {
            _generation = 0;
            _players = new List<GeneticNeuralNetworkController>();
        }

        public void Initialize(Rectangle screenBounds)
        {
            _players.Clear();
            _environmentContext = new EnvironmentContext(new Ground(_groundTexture, new Vector2(0, 300)), screenBounds, _obstacleTexture);

            for (int i = 0; i < PlayerCount; i++)
            {
                var network = new GeneticNeuralNetworkController();
                Character character = _environmentContext.AddCharacter(_characterTexture, new GeneticNeuralNetworkController());
                network.Character = character;
                _players.Add(network);
            }
        }

        public void LoadContent(ContentManager content)
        {
            _characterTexture = content.Load<Texture2D>("character");
            _groundTexture = content.Load<Texture2D>("ground");
            _obstacleTexture = content.Load<Texture2D>("Motorcycle");
        }

        public void Update(GameTime gameTime)
        {
            if (!_environmentContext.Characters.All(x => x.Dead))
            {
                _environmentContext.Update(gameTime);
            }
            else
            {
                _highScore = Math.Max(_highScore, _environmentContext.Characters.Max(x => x.Score));
                NatularSelection();
                for (int i = 0; i < _players.Count - 1; i++)
                {
                    _players[i].MutateNetwork();
                }

                _environmentContext.Initialize();
                foreach (GeneticNeuralNetworkController player in _players)
                {
                    player.Character = _environmentContext.AddCharacter(_characterTexture, player);
                }
                _generation++;
            }
        }

        private void NatularSelection()
        {
            GeneticNeuralNetworkController[] orderedPlayers = _players.OrderByDescending(x => x.CalculateFitness()).ToArray();

            var sumFitness = _players.Sum(x => x.CalculateFitness());
            var newPlayers = new List<GeneticNeuralNetworkController>();

            GeneticNeuralNetworkController best = orderedPlayers.First();


            for(int i = 0; i < orderedPlayers.Length - 1; i++)
            {
                GeneticNeuralNetworkController player = SelectPlayer(sumFitness, orderedPlayers);
                newPlayers.Add(player);
            }

            // Keep the best player around
            newPlayers.Add(best);

            _players.Clear();
            _players.AddRange(newPlayers);
        }

        private GeneticNeuralNetworkController SelectPlayer(double sumFitness, GeneticNeuralNetworkController[] orderedPlayers)
        {
            double runningSum = 0;
            var rand = _random.NextDouble() * sumFitness;
            foreach (GeneticNeuralNetworkController player in orderedPlayers)
            {
                runningSum += player.CalculateFitness();
                if (runningSum > rand)
                {
                    return player.Clone();
                }
            }

            throw new Exception("Unable to select player...");
        }

        public void Draw(GameTime _, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextHelper.Font, $"Gen: {_generation}", new Vector2(50, 25), Color.Black);
            spriteBatch.DrawString(TextHelper.Font, $"Dead: {_environmentContext.Characters.Count(x => x.Dead)}", new Vector2(50, 75), Color.Black);
            spriteBatch.DrawString(TextHelper.Font, $"High Score: {_highScore:N0}", new Vector2(_environmentContext.GameBounds.Right - 150, 25), Color.Black);
            _environmentContext.Draw(spriteBatch);
        }

        
    }
}
