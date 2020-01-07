using System;
using System.Threading;
using System.IO;

namespace GameLifeCSharpConsole
{
    class Field
    {
        private int _width, _height;
        private char _render;
        private Cell[,] _cells;

        public Field(int height, int width, char render = 'o')
        {
            _width = width; //x
            _height = height; //y
            _render = render;
            _cells = new Cell[_height, _width];
        }

        public void Init(string filename)
        {
            string[] rows = File.ReadAllLines(filename);
            // Checking the input:
            CheckFieldHeight(rows);
            CheckFieldWidth(rows);
            CheckInputSymbols(rows);// Checking if the file consists only of 1's and 0's.
            CheckActiveInputCells(rows);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _cells[y, x] = new Cell(rows[y][x] == '1');
                }
            }
        }


        public void DrawField()
        {
            Console.Clear();

            DrawUpperBorderLine();
            for (int y = 0; y < _height; y++)
            {
                Console.Write('║');
                for (int x = 0; x < _width; x++)
                {
                    _cells[y, x].ChangeGeneration();
                    Console.Write(_cells[y, x].IsActive ? _render : ' ');
                }
                Console.Write('║');
                Console.WriteLine();
            }
            DrawLowerBorderLine();
        }

        public void GoToNextGeneration()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int activeNeightboors = GetActiveNeighbors(x, y);
                    _cells[y,x].PredictCellStatus(activeNeightboors);
                }
            }
        }

        public bool AreAllCellsDead()
        {
            bool hasAnyActiveCells = true;
            foreach (Cell cell in _cells)
            {
                if (cell.IsActive || cell.WillBeActive)
                {
                    hasAnyActiveCells = true;
                }
            }
            return hasAnyActiveCells;
        }

        private int GetActiveNeighbors(int xPos, int yPos)
        {
            int activeCells = 0;

            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (x == 0 && y == 0) // current cell position. cant be count as an active neighbor.
                    {
                        continue;
                    }

                    int yNeighborPos = (yPos + y + _height) % _height;
                    int xNeighborPos = (xPos + x + _width) % _width;

                    activeCells += Convert.ToInt32(_cells[yNeighborPos, xNeighborPos].IsActive);
                }
            }

            return activeCells;
        }    

        private void CheckFieldHeight(string[] rows)
        {
            if (rows.Length != _height)
            {
                throw new Exception(string.Format($"The input file must have {_height} lines, every {_width} symbols long."));
            }
        }

        private void CheckFieldWidth(string[] rows)
        {
            foreach (string row in rows)
            {
                // Checking if every line of input has correct width.
                if (row.Length != _width)
                {
                    throw new Exception(string.Format($"Every line must be {_width} symbols long."));
                }
            }
        }

        private void CheckInputSymbols(string[] rows)
        {
            foreach (string row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] != '0' && row[i] != '1')
                    {
                        throw new Exception(string.Format("The input file must consist only of '1' and '0' symbols."));
                    }
                }
            }
        }

        private void CheckActiveInputCells(string[] rows)
        {
            bool hasAnyActiveCells = false;
            foreach (string row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == '1')
                    {
                        hasAnyActiveCells = true;
                    }
                }
            }

            if(!hasAnyActiveCells)
            {
                throw new Exception(string.Format("Every input field must contain at least one active cell! Add some using the '1' symbol."));
            }
        }

        private void DrawUpperBorderLine()
        {
            Console.Write('╔' + new String('═', _width) + '╗');
            Console.WriteLine();
        }

        private void DrawLowerBorderLine()
        {
            Console.Write('╚' + new String('═', _width) + '╝');
            Console.WriteLine();
        }
    }
}
