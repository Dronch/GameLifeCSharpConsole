﻿using System;
using System.Threading;
using System.IO;

namespace GameLifeCSharpConsole
{
    public class Field
    {
        private int _width, _height;
        private char _render;
        private Cell[,] _cells;
        private bool _fixErrors;

        public int Width { get => _width; private set => _width = value; }
        public int Height { get => _height; private set => _height = value; }

        public Cell GetCell(int yPos, int xPos)
        {
            return _cells[yPos, xPos];
        }

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

            // This check needs only length of rows array. No need to put in in a cycle.
            // We pass ref, because we want be able to update rows if _fixErrors = true
            CheckFieldHeight(ref rows); 

            //Next 3 checks will need access to every row of rows[] array. So we put em all in a foreach cycle.
            for (int i = 0; i < rows.Length; i++)
            {
                CheckFieldWidth(ref rows[i]);
                CheckInputSymbols(ref rows[i]);
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
            try
            {
                Console.Clear();
            }

            catch (IOException)
            {

            }

            bool hasAnyActiveCells = false;

            DrawUpperBorderLine();
            for (int y = 0; y < _height; y++)
            {
                Console.Write('║');
                for (int x = 0; x < _width; x++)
                {
                    _cells[y, x].ChangeGeneration();
                    Console.Write(_cells[y, x].IsActive ? _render : ' ');

                    if (_cells[y, x].IsActive)
                    {
                        hasAnyActiveCells = true;
                    }
                }
                Console.Write('║');
                Console.WriteLine();
            }
            DrawLowerBorderLine();

            if(!hasAnyActiveCells)
            {
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

        private void CheckFieldHeight(ref string[] rows)
        {      
            // Valid height
            if (rows.Length == _height)
            {
                return;
            }

            if (_fixErrors)
            {
                int tailLength = rows.Length < _height ? _height - rows.Length : 0;
                if (tailLength > 0)
                {
                    string[] tail = new string[tailLength];
                    for (int i = 0; i < tail.Length; i++)
                    {
                        tail[i] = new String('0', _width);
                    }
                    string[] updatedRows = new string[_height];
                    rows.CopyTo(updatedRows, 0);
                    tail.CopyTo(updatedRows, rows.Length);
                    rows = updatedRows;
                }
            }
            else
            {
                throw new InvalidFieldHeightException(rows.Length, _height);
            }
        }

        private void CheckFieldWidth(ref string row)
        {
            // Valid row width
            if (row.Length == _width)
            {
                return;
            }

            if (_fixErrors)
            {
                int tailLength = row.Length < _width ? _width - row.Length : 0;
                if (tailLength > 0)
                {
                  row += new String('0', tailLength);
                }
            }
            else
            {
                throw new InvalidFieldWidthException(row.Length, _width);
            }
        }

        private void CheckInputSymbols(ref string row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                // Valid symbol
                if (row[i] == '0' || row[i] == '1')
                {
                    continue;
                }

                if (!_fixErrors)
                {
                    throw new InvalidInputSymbolsException(row[i]);
                }
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

