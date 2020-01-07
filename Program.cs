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

            RunTheGameOfLife(field);
        }

        static void RunTheGameOfLife(Field field)
        {
            int generation = 0;
            bool AllCellsAreDead = false;
            do
            {
                try
                {
                    field.DrawField();
                }

                catch (NoActiveCellsException)
                {
                    Console.WriteLine("No active cells left on the field! Game over.");
                    AllCellsAreDead = true;
                }

                Console.WriteLine();
                Console.WriteLine(String.Format("Generation: {0}", generation++));
                field.GoToNextGeneration();
                Thread.Sleep(1000);
            }
            while (!AllCellsAreDead && Console.KeyAvailable == false);

            Console.WriteLine("Finished");
        }
    }
}
