using System.Collections;
using System.Collections.Generic;
using Logic.interfaces;

namespace Logic.Calculators
{
    public partial class NewtonDerivative
    {
        public static IList<Point> CalculatePoints(IBaseMathOperator oper, double h, double spaceX, double startX, double endX)
        {
            var list = new List<Point>();
            for (var x = startX; x <= endX; x += spaceX)
            {
                var point = new Point
                {
                    X = x,
                    Y = GetYValue(oper, x, h)
                };
                list.Add(point);
            }
            return list;
        }

        private static double GetYValue(IBaseMathOperator oper, double x, double h)
        {
            oper.SetXValue(x + h);
            var first = oper.Result();
            
            oper.SetXValue(x);
            var second = oper.Result();
            return (first - second) / h;
        }
    }
}