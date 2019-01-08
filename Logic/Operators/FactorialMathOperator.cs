using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class FactorialMathOperator : AbstractConstantMathOperator
    {
        private readonly double result;
        public FactorialMathOperator(double d) : base(d)
        {
            
            data = (uint) Math.Round(Math.Abs(d));
            result = 1;
            for (var i = 1; i <= data; i++)
            {
                result *= i;
            }
        }

        public override double Result()
        {
            return result;
        }

        public override void SetXValue(double number){}

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
            return result.ToString();
        }

        public override string MathSymbol()
        {
            return "!";
        }

        public override IBaseMathOperator Derivate()
        {
            return new ConstantMathOperator(0); //derivate of a constant is 0
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
            if (result < 1000)
            {
                return new ConstantMathOperator(result);
            }
            return new FactorialMathOperator(data);
        }
    }
}