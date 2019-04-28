using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniRun.GameObjects;
using InfiniRun.Managers;

namespace InfiniRun.Controlls
{
    public interface IInputController
    {
        Command GetCommand(Character actor, EnvironmentContext environment);
    }
}
