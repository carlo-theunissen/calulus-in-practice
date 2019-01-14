using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Logic.Calculators;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebView.json;
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
        
        public IActionResult Calculate(string formula, string points, double taylorAround, uint taylorN, double startIntegral, double endIntegral)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return RedirectToAction("Index");
            }
            
            var model = new CalculateViewModel();

            model.Original = formula;
            
            
            var parser = new StringParser(formula);
            var result = parser.GetOperator();
            model.NiceFormat = (result.DeepSimplyfy().ToMathString());
            
            model.DerivativeNiceFormat = (result.Derivate().DeepSimplyfy().ToMathString());
            model.DerivativePointJson =
                JSONHelper.ToJSON(NewtonDerivative.CalculatePoints(result, 0.001d, 0.5d, -3, 3));
            
            model.IntegralStart = startIntegral;
            model.IntegralEnd = endIntegral;

            model.IntegralSum = RiemannSum.CalculateSum(result, startIntegral, endIntegral);

            model.TaylorPoloynoomAround = taylorAround;
            model.TaylorPoloynoomNiceFormat = Taylorpolynomial.CalculateNPolynomial(result, taylorAround, taylorN).ToMathString();
            
            model.McClairenPoloynoomNiceFormat = Taylorpolynomial.CalculateNPolynomial(result, 0, taylorN).ToMathString();

            model.Request = HttpContext;
            try
            {
                model.GausJordon = GausJordan.GetBaseMathOperatorFromList(GetPoints(points)).DeepSimplyfy()
                    .ToMathString();
                model.GausJordonFault = false;
            }
            catch (Exception e)
            {
                model.GausJordonFault = true;
            }
            
            var json = JsonCreator.CreateFromBaseOpeator(result);
            model.JsonData = JsonConvert.SerializeObject(json);

            model.TaylorPossible = model.McClairenPoloynoomNiceFormat.IndexOf("NaN", StringComparison.Ordinal) >= 0 || model.TaylorPoloynoomNiceFormat.IndexOf("NaN", StringComparison.Ordinal) >=0 ;
            
            return View(model);
        }

        private IList<Point> GetPoints(string pointArray)
        {
            return pointArray.Split(";").Select(x =>
            {
                var split = x.Split(",");
                return new Point()
                {
                    X = double.Parse(split[0]),
                    Y = double.Parse(split[1])
                };
            }).ToList();
        }
    }
}