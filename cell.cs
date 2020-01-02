using System;


namespace GameLife {

  public class Cell
  {
    public bool is_active {get {return _is_active;}}

    private bool _is_active;
    private bool _will_be_active;

    public Cell(bool active) => _will_be_active = active; 

    public void predict_generation (ref Cell[] neighbors)
    {
      // TODO implement gamelife cell logic
      _will_be_active = !is_active; // mock
    }

    public void change_generation()
    {
      _is_active = _will_be_active;
    }
  }

}