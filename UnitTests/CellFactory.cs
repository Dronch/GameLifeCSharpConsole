using System;
using System.Collections.Generic;
using System.Text;
using GameLifeCSharpConsole;

namespace GameLifeCSharpConsole.UnitTests
{
    public class CellFactory
    {
        /// <param name="active"> 
        /// Value "true" to get a living (will be active next turn) cell. 
        /// Value "false" to get a dead (won't be active next turn) cell.</param>
        public static Cell Create(bool active)
        {
            Cell instance = new Cell(active);
            instance.ChangeGeneration();
            return instance;
        }
    }
}
