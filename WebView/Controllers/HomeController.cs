using System;
using System.Linq;
using Logic.Graph;
using Microsoft.AspNetCore.Mvc;
using WebView.Models;

namespace WebView.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new CalculateViewModel();
            model.Request = HttpContext;
            
            Console.WriteLine("formula:" + "test");
            return View(model);
        }
        
        public IActionResult Calculate(int nodes, double prob)
        {
            if (nodes == 0)
            {
                return RedirectToAction("Index");
            }

            var graph = new GraphGenerator().GetGraph(nodes, prob);
            
            var model = new CalculateViewModel();
            model.Request = HttpContext;
            model.Nodes = graph;
            model.Connected = BFS.Connected(graph.First()).Count == graph.Count;
            model.Prob = new ProbCalculator().GetTable(nodes, 100, 1 / 100d);
            model.Lambda = 1 / 100d;
            model.Prediction = Math.Log(nodes) / nodes;
            
            return View(model);
        }


    }
}