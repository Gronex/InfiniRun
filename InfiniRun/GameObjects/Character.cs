using System.Linq;
using InfiniRun.Controlls;
using InfiniRun.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniRun.GameObjects
{
    public class Character : GameObject
    {
        private Vector2 _velocity;
        private bool _jumping;
        private readonly IInputController _controller;

        public bool Dead { get; private set; }

        public double Score { get; private set; }

        public Character(Texture2D sprite, Vector2 position, IInputController controller) : base(sprite, position)
        {
            _velocity = Vector2.Zero;
            _controller = controller;
        }

        public override void Update(GameTime gameTime, EnvironmentContext environmentContext)
        {
            if (Dead)
            {
                return;
            }

            Move(environmentContext);

            if (environmentContext.Obstacles.Any(x => x.Bounds.Intersects(Bounds)))
            {
                Dead = true;
                return;
            }

            Score += gameTime.ElapsedGameTime.TotalSeconds * 10;
        }

        private void Move(EnvironmentContext environmentContext)
        {
            _velocity = GravityHelper.ApplyGravity(_velocity);

            switch (_controller.GetCommand(this, environmentContext))
            {
                case Command.Jump:
                    if (!_jumping)
                    {
                        _velocity += new Vector2(0, -5f);
                        _jumping = true;
                    }
                    break;

                case Command.None:
                    // Ignore
                    break;
            }
            Vector2 newPosition = Position + _velocity;

            if (_velocity.Y > 0 && environmentContext.Ground.Bounds.Contains(newPosition + new Vector2(0, Bounds.Height)))
            {
                _velocity = Vector2.Zero;
                _jumping = false;
            }
            else
            {
                Position = newPosition;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Dead)
            {
                base.Draw(spriteBatch);
                spriteBatch.DrawString(TextHelper.Font, $"Score: {Score:N0}", new Vector2(50), Color.Black);
            }

        }
    }
}
