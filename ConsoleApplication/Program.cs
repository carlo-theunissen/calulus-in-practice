using System;
using System.Collections.Generic;
using System.Diagnostics;
using Logic;
using Logic.Calculators;


namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                var list = new List<Point>();
                list.Add(new Point(){ X = 7, Y = 3});
                list.Add(new Point(){ X = 5, Y = 6});
                list.Add(new Point(){ X = 2, Y = 7});
                var mat = GausJordan.GetMatrixFromPoints(list);
                var ys = GausJordan.GetAnwsersFromPoints(list);
                
                var ans = GausJordan.GetValues(mat, ys);
                
                foreach (var an in ans)
                {
                    Console.WriteLine(an);
                }
                
                Console.WriteLine(GausJordan.GetBaseMathOperatorFromValues(ans).DeepSimplyfy().ToMathString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}