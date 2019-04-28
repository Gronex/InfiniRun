using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniRun.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniRun.GameObjects
{
    public class Obstacle : GameObject
    {
        private static readonly Random random = new Random();
        private Color _color;

        public Obstacle(Texture2D sprite, Vector2 position, Vector2 velocity) : base(sprite, position)
        {
            Velocity = velocity;
            _color = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

        public Vector2 Velocity { get; set; }

        public override void Update(GameTime gameTime, EnvironmentContext environmentContext)
        {
            Position += Velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, _color);
        }
    }
}
