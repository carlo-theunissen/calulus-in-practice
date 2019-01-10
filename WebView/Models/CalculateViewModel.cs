using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebView.Models
{
    public class CalculateViewModel
    {
        public List<int[]> graph = new List<int[]>();

        public HttpContext Request { get; set; }
    }
}