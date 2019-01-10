using System;
using System.Linq;

namespace Logic.Graph
{
    public class ProbCalculator
    {
        public int[] GetTable(int nodeCount, int tries, double lambda)
        {
            var result = new int[(int) Math.Ceiling( 1 / lambda)];
            var index = 0;
            for (double i = 0; i <= 1; i += lambda)
            {
                for (var x = 0; x < tries; x++)
                {
                    if (IsConnected(nodeCount, i))
                    {
                        result[index]++;
                    }
                }
                index++;
            }
            return result;
        }

        private bool IsConnected(int nodeCount, double prob)
        {
            var graph = new GraphGenerator().GetGraph(nodeCount, prob);
            return BFS.Connected(graph.First()).Count == graph.Count;
        }
        
    }
}