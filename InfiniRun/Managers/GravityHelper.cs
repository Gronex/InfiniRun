using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace InfiniRun.Managers
{
    public static class GravityHelper
    {
        private static readonly Vector2 s_gravity = new Vector2(0, 0.3f);

        public static Vector2 ApplyGravity(Vector2 velocity)
        {
            return velocity + s_gravity;
        }
    }
}
