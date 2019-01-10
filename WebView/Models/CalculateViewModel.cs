using System.Collections.Generic;
using Logic.Models;

namespace WebView.Models
{
    public class CalculateViewModel
    {
   
        public object Request { get; set; }
        public List<Node> Nodes { get; set; }
        public bool Connected { get; set; }
        public int[] Prob { get; set; }
        public double Lambda { get; set; }
        public double Prediction { get; set; }
    }
}