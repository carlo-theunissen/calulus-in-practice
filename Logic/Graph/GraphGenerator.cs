using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Models;

namespace Logic.Graph
{
    public class GraphGenerator
    {
        public List<Node> GetGraph(int size, double prob)
        {
            var nodes = new List<Node>(size);
            
            for (var i = 0; i < size; i++)
            {
                nodes.Add(new Node(i));
            }
            
            for (var i = 0; i < nodes.Count; i++)
            {
                for (var y = i+1; y < nodes.Count; y++)
                {
                    if (Valid(prob))
                    {
                        nodes[i].Sibblings.Add(nodes[y]);
                        nodes[y].Sibblings.Add(nodes[i]);
                    }
                }
            }
            return nodes;
        }

        private bool Valid(double prod)
        {
            return new Random().NextDouble() <= prod;
        }
    }
}