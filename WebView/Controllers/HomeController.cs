using System;
using Logic.Probability;
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
        
        public IActionResult Calculate(int tries, int poissonTries, double poissonLambda, int interval)
        {
            if (tries == 0)
            {
                return RedirectToAction("Index");
            }
            
            var model = new CalculateViewModel();
            model.Request = HttpContext;
            var poisson = new BasicGraph(new PosionGenerator(poissonTries, poissonLambda), tries);
            model.graph.Add(poisson.GetSteps());

            var normal  = new BasicGraph(new UniformGenerator(0, 100), tries);
            model.graph.Add(normal.GetSteps());
            
            var expo  = new BasicGraph(new ExponentialGenerator(poissonLambda, 10), tries);
            model.graph.Add(expo.GetSteps());    
            
            var converge  = new BasicGraph(new ConvergePossion(new ExponentialGenerator(poissonLambda, 1), interval), tries);
            var cache = converge.GetSteps();
            model.graph.Add(cache);
            
            return View(model);
        }


    }
}