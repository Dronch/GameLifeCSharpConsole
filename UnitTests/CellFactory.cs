using System;
using System.Collections.Generic;
using System.Text;
using GameLifeCSharpConsole;

namespace GameLifeCSharpConsole.UnitTests
{
    public class CellFactory
    {
        public static Cell Create(bool active)
        {
            Cell instance = new Cell(active);
            instance.ChangeGeneration();
            return instance;
        }
    }
}
