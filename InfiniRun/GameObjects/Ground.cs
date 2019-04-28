using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniRun.GameObjects
{
    public class Ground : GameObject
    {
        public Ground(Texture2D sprite, Vector2 position) : base(sprite, position)
        {
        }
    }
}
