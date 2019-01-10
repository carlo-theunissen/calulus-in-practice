using System;
using System.Collections.Generic;
using Logic.Graph;
using Logic.Models;


namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var nodes = new List<Node>();
                var connected1 = new Node(1);
                var connected2 = new Node(2);
               
                connected1.Sibblings.Add(connected2);
                connected2.Sibblings.Add(connected1);
                var result = BFS.Connected(connected1);
                Console.WriteLine(result.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}