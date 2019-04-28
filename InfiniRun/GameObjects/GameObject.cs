using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniRun.GameObjects
{
    public abstract class GameObject
    {
        private Rectangle _bounds;

        public GameObject(Texture2D sprite, Vector2 position)
        {
            Position = position;
            Sprite = sprite;
            _bounds = new Rectangle();
        }

        public Vector2 Position { get; set; }

        public Vector2 Scale { get; set; } = Vector2.One;

        public float Rotation { get; set; }

        public Texture2D Sprite { get; set; }

        public Rectangle Bounds
        {
            get
            {
                _bounds.Location = Position.ToPoint();
                _bounds.Size = Sprite.Bounds.Size;
                return _bounds;
            }
        }

        public virtual void Update(GameTime gameTime, Managers.EnvironmentContext environmentContext)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
