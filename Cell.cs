using System;

namespace GameLifeCSharpConsole
{
    class Cell
    {
        public bool IsActive 
        { 
            get { return _isActive; } 
            private set { _isActive = value; } 
        }

        public bool WillBeActive
        {
            get { return _willBeActive; }
            private set { _willBeActive = value; }
        }

        private bool _isActive;
        private bool _willBeActive;

        public Cell(bool isActive) => WillBeActive = isActive;

        public void ChangeGeneration()
        {
            IsActive = WillBeActive;
        }

        public void PredictCellStatus(int activeNeighbors)
        {
            bool willBeBorn = !IsActive && activeNeighbors == 3;
            bool willDie = IsActive && (activeNeighbors < 2 || activeNeighbors > 3);

            if (willBeBorn)
            {
                WillBeActive = true;
            }

            else if (willDie)
            {
                WillBeActive = false;
            }

            else
            {
                WillBeActive = IsActive;
            }
        }
    }
}
