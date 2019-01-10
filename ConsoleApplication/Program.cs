using System;
using Logic.Probability;


namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                var numbers = new BasicGraph(new PosionGenerator(10000, 0.5f), 100);
                foreach (var step in numbers.GetSteps())
                {
                       Console.WriteLine(step);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}