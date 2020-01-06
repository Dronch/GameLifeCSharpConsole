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
            _width = width; //x (j)
            _height = height; //y (i)
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

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _cells[i, j] = new Cell(rows[i][j] == '1');
                }
            }
        }


        public void DrawField()
        {
            Console.Clear();

            for (int i = 0; i < _height; i++)
            {             
                if (i == 0)
                    DrawUpperBorderLine();

                for (int j = 0; j < _width; j++)
                {
                    //Drawing the left side of the border.
                    if (j == 0)
                        Console.Write('║');

                    //Drawing active and incative cells.
                    _cells[i, j].ChangeGeneration();
                    Console.Write(_cells[i, j].IsActive ? _render : ' ');

                    //Drawing the right side of the border.
                    if (j == _width - 1)
                        Console.Write('║');
                }

                Console.WriteLine();

                if (i == _height - 1)
                    DrawLowerBorderLine();
            }
            
        }

        public void GoToNextGeneration()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    int activeNeightboors = GetActiveNeighbors(i, j);
                    _cells[i,j].PredictCellStatus(activeNeightboors);
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

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (j == 0 && i == 0) // current cell position. cant be count as an active neighbor.
                    {
                        continue;
                    }

                    int yNeighborPos = (yPos + i + _height) % _height;
                    int xNeighborPos = (xPos + j + _width) % -_width;

                    if (_cells[yNeighborPos, xNeighborPos].IsActive)
                    {
                        activeCells++;
                    }
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

            Console.Write('╔');
            for (int z = 0; z < _width; z++)
            {
                Console.Write('═');
            }
            Console.Write('╗');
            Console.WriteLine();
        }

        private void DrawLowerBorderLine()
        {
            Console.Write('╚');
            for (int z = 0; z < _width; z++)
            {
                Console.Write('═');
            }
            Console.Write('╝');
            Console.WriteLine();
        }
    }
}
