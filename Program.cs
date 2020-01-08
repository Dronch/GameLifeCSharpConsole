using System;
using System.Threading;

namespace GameLifeCSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTheGameOfLife("input.txt", 20, 40, false);
        }

        static void RunTheGameOfLife(string filename, int height, int width, bool fixErrors=false)
        {
            try
            {
                int generation = 0;
                Field field = new Field(height, width, fixErrors);
                field.Init(filename);
                do
                {
                    field.DrawField();
                    Console.WriteLine();
                    Console.WriteLine(String.Format("Generation: {0}", generation++));
                    field.GoToNextGeneration();
                    Thread.Sleep(1000);
                }
                while (Console.KeyAvailable == false);
                Console.WriteLine("Quit");
            }
            catch (NoActiveCellsException)
            {
                Console.WriteLine("No active cells left on the field! Game over.");
            }
            catch (Exception e)
            {
                if (e is InvalidFieldHeightException || e is InvalidFieldWidthException || e is InvalidInputSymbolsException)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Do you want to fix errors automaticly? (y/n)");
                    if (Console.ReadLine() == "y")
                    {
                        RunTheGameOfLife(filename, height, width, true);
                    }
                    else
                    {
                        Console.WriteLine("Quit");
                    }
                }
                else
                {
                  Console.Clear();
                  Console.WriteLine(string.Format($"Internal error: {e.Message}"));
                }
            }
        }
    }
}

