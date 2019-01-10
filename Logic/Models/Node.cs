using System.Collections.Generic;

namespace Logic.Models
{
    public class Node
    {
        public List<Node> Sibblings = new List<Node>();
        public int Id { get; }

        public Node(int id)
        {
            this.Id = id;
        }
    }
}