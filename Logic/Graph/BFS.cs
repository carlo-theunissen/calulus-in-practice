using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logic.Models;

namespace Logic.Graph
{
    public class BFS
    {
        public static List<Node> Connected(Node node)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(node);
            var nodes = new List<Node>();
            nodes.Add(node);
            while (queue.Count > 0)
            {
                var work = queue.Dequeue();
                foreach (var workSibbling in work.Sibblings)
                {
                    if (nodes.Any(x => x.Id == workSibbling.Id)) continue;
                    
                    nodes.Add(workSibbling);
                    queue.Enqueue(workSibbling);
                }
            }
            return nodes;
        }
    }
}