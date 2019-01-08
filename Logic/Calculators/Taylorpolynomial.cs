using System;
using System.Collections.Generic;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Calculators
{
    public class Taylorpolynomial
    {
        public static IBaseMathOperator CalculateNPolynomial(IBaseMathOperator original, double x, uint n)
        {
            var addList = new List<IBaseMathOperator>();
            var lastDerivative = original;
            original.SetXValue(x);
            addList.Add(new ConstantMathOperator(original.Result()));
            for (var i = 1; i < n; i++)
            {
                var times = new MultiplyMathOperator();
                
                {
                    var der = lastDerivative.Derivate();
                    lastDerivative = der;
                    
                }
                
                lastDerivative.SetXValue(x);
                
                var divide = new DevideMathOperator();
                var factorial = new FactorialMathOperator(i);
                divide.Instantiate(new IBaseMathOperator[]{new ConstantMathOperator(lastDerivative.Result()), factorial });
                
                var minus = new MinMathOperator();
                minus.Instantiate(new IBaseMathOperator[]{new VariableXMathOperator(), new ConstantMathOperator(x) });
                
                var power = new ExpotentialOperator();
                power.Instantiate(new IBaseMathOperator[]{minus, new ConstantMathOperator(i)});
                
                times.Instantiate(new IBaseMathOperator[]{divide, power});
                addList.Add(times.DeepSimplyfy());
                
            }
            
            return new AddMathOperator().CreateFromList(addList).DeepSimplyfy();
        }
    }
}