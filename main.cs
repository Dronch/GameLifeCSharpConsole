using System;
using System.Threading;
using GameLife;

					
public class Program
{
	public static void Main()
	{
    Field field = new Field(20, 40);
    field.init("input.txt");

    int generation = 0;
    do
    {
      field.draw();
      Console.WriteLine();
      Console.WriteLine(String.Format("Generation: {0}", generation++));
      field.next_generation();
      Thread.Sleep(1000);
    } while(Console.KeyAvailable == false);

    Console.WriteLine("Finished");
    
	}
}