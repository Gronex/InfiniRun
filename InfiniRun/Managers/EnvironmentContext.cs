using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniRun.Controlls;
using InfiniRun.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniRun.Managers
{
    public class EnvironmentContext
    {
        private readonly Random _random = new Random();
        private List<Obstacle> _obstacles;
        private readonly Texture2D _obstacleTexture;

        private Vector2 _obstacleVelocity;

        public EnvironmentContext(Ground ground, Rectangle gameBounds, Texture2D obstacleTexture)
        {
            Ground = ground;
            GameBounds = gameBounds;
            _obstacleTexture = obstacleTexture;
            Initialize();
        }

        public Ground Ground { get; }

        public ICollection<Character> Characters { get; private set; }

        public IEnumerable<Obstacle> Obstacles => _obstacles;

        public Rectangle GameBounds { get; }

        public void Update(GameTime gameTime)
        {
            AddObstacles();

            Ground.Update(gameTime, this);
            foreach (Obstacle obstacle in Obstacles)
            {
                obstacle.Update(gameTime, this);
            }
            _obstacles.RemoveAll(x => !x.Bounds.Intersects(GameBounds));

            foreach(Character character in Characters)
            {
                character.Update(gameTime, this);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Ground.Draw(spriteBatch);
            foreach(Obstacle obstacle in Obstacles)
            {
                obstacle.Draw(spriteBatch);
            }
            foreach(Character character in Characters)
            {
                character.Draw(spriteBatch);
            }
        }

        public Character AddCharacter(Texture2D texture, IInputController inputController)
        {
            var character = new Character(texture, new Vector2(50, Ground.Bounds.Top - texture.Height), inputController);
            Characters.Add(character);
            return character;
        }

        internal void Initialize()
        {
            _obstacles = new List<Obstacle>();
            _obstacleVelocity = new Vector2(-5, 0);
            Characters = new List<Character>();
        }

        private void AddObstacles()
        {
            var rand = _random.Next(0, 200);

            var spawnPosition = new Vector2(GameBounds.Right, Ground.Bounds.Top - _obstacleTexture.Height);

            Obstacle newestObstacle = Obstacles.LastOrDefault();

            var distance = spawnPosition.X - newestObstacle?.Position.X;
            if (newestObstacle == null || distance >= 175 + rand)
            {
                _obstacles.Add(new Obstacle(_obstacleTexture, spawnPosition, _obstacleVelocity));
            }

        }
    }
}
