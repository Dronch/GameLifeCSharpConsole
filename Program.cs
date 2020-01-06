using System;
using System.Threading;

namespace GameLifeCSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field(20, 40);
            field.Init("input.txt");

            int generation = 0;
            do
            {
                field.DrawField();
                Console.WriteLine();
                Console.WriteLine(String.Format("Generation: {0}", generation++));
                field.GoToNextGeneration();
                Thread.Sleep(2000);
            }
            while (field.AreAllCellsDead() && Console.KeyAvailable == false);
            {
                
            }

            Console.WriteLine("Finished");
        }
    }
}
