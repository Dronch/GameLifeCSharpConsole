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
        private bool _fixErrors;

        public Field(int height, int width, bool fixErrors=false, char render = 'o')
        {
            _width = width; //x
            _height = height; //y
            _render = render;
            _cells = new Cell[_height, _width];
            _fixErrors = fixErrors;
        }

        public void Init(string filename)
        {
            string[] rows = File.ReadAllLines(filename);
            // Checking the input:

            //This check needs only length of rows array. No need to put in in a cycle.
            CheckFieldHeight(rows); 

            //Next 3 checks will need access to every row of rows[] array. So we put em all in a foreach cycle.
            foreach(string row in rows)
            {
                CheckFieldWidth(row);
                CheckInputSymbols(row);
                CheckActiveInputCells(row);
            }

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
            
            bool hasAnyActiveCells = false;

            DrawUpperBorderLine();
            for (int y = 0; y < _height; y++)
            {
                Console.Write('║');
                for (int x = 0; x < _width; x++)
                {
                    if (_cells[y, x].IsActive || _cells[y, x].WillBeActive)
                    {
                        hasAnyActiveCells = true;
                    }
                    
                    _cells[y, x].ChangeGeneration();
                    Console.Write(_cells[y, x].IsActive ? _render : ' ');
                }
                Console.Write('║');
                Console.WriteLine();
            }
            DrawLowerBorderLine();

            if(!hasAnyActiveCells)
            {
                Console.Clear();
                throw new NoActiveCellsException();
            }
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
                throw new InvalidFieldHeightException(rows.Length, _height);
            }
        }

        private void CheckFieldWidth(string row)
        {
            if (row.Length != _width)
            {
                throw new InvalidFieldWidthException(row.Length,_width);
            }
        }

        private void CheckInputSymbols(string row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] != '0' && row[i] != '1')
                {
                    throw new InvalidInputSymbolsException(row[i]);
                }
            }
        }

        private void CheckActiveInputCells(string row)
        {
            bool hasAnyActiveCells = false;

            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == '1')
                {
                    hasAnyActiveCells = true;
                }
            }

            if(!hasAnyActiveCells)
            {
                throw new NoActiveCellsException();
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

