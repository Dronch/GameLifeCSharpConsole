using System;
using System.Threading;


namespace GameLife {

  public class Field
  {

    private int _width, _height;
    private char _render;
    private Cell[,] _cells;

    public Field(int height, int width, char render = 'o')
    {
      _height = height;
      _width = width;
      _render = render;
      _cells = new Cell[_height, width];
    }

    public void init(string filename)
    {
      // TODO throw exceptions if input is not valid
      string[] rows = System.IO.File.ReadAllLines(filename);
      for (int i = 0; i < _height; i++)
      {
        for (int j = 0; j < _width; j++)
        {
          _cells[i, j] = new Cell(rows[i][j] == '1');
        }
      }
    }

    public void draw()
    {
      Console.Clear();
      // TODO render field border
      for (int i = 0; i < _height; i++)
      {
        for (int j = 0; j < _width; j++)
        {
          _cells[i, j].change_generation();
          Console.Write(_cells[i, j].is_active ? _render : ' ');
        }
        Console.WriteLine();
      }
    }

    public void next_generation()
    {
      for (int i = 0; i < _height; i++)
      {
        for (int j = 0; j < _width; j++)
        {
          Cell[] neighbors = new Cell[8];
          // TODO get cell neighbors
          _cells[i, j].predict_generation(ref neighbors);
        }
      }
    }

  }

}