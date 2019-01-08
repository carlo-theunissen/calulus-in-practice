using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class VariableXMathOperator : AbstractBaseMathOperator
    {
        protected double multiply = 1;
        protected double value = 1;

        public override double Result()
        {
            return Round(value * multiply);
        }

        public void SetMultiply(double number)
        {
            multiply = number;
        }

        public double GetMultiply()
        {
            return multiply;
        }
        
        public override void SetXValue(double number)
        {
            value = number;
        }

        public override IBaseMathOperator[] GetChilds()
        {
            return new IBaseMathOperator[0];
        }

        public override int GetOperatorNeededArguments()
        {
            return 0;
        }

        public override string ToMathString()
        {
            return Math.Abs(multiply - 1) > 0.00001d ? multiply+"x" : "x";
        }

        public override string MathSymbol()
        {
            return ToMathString();
        }

        public override IBaseMathOperator Derivate()
        {
            return new ConstantMathOperator(multiply);
        }

        private IBaseMathOperator cachedSimplyfy = null;
        public override IBaseMathOperator Simplyfy()
        {
            if (cachedSimplyfy != null)
            {
                return cachedSimplyfy;
            }
            return cachedSimplyfy = CalculateSimplyfy();
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            if (Math.Abs(multiply) < 0.00001d)
            {
                return new ConstantMathOperator(0);
            }
            var temp = new VariableXMathOperator();
            temp.SetMultiply(multiply);
            temp.SetXValue(value);
            return temp;
        }
    }
}