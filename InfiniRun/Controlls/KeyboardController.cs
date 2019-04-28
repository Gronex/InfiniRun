using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniRun.GameObjects;
using InfiniRun.Managers;
using Microsoft.Xna.Framework.Input;

namespace InfiniRun.Controlls
{
    public class KeyboardController : IInputController
    {
        public Command GetCommand(Character actor, EnvironmentContext environment)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Up))
            {
                return Command.Jump;
            }

            return Command.None;
        }
    }
}
