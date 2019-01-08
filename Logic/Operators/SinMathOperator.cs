using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class SinMathOperator : AbstractSingleMathOperator
    {

        public override double Result()
        {
            var a = A.Result();
            return  Round(Math.Sin(a));
        }

        public override string ToMathString()
        {
            return "sin("+A.ToMathString()+")" ;
        }

        public override string MathSymbol()
        {
            return "sin";
        }
        public override IBaseMathOperator CalculateSimplyfy()
        {
            var sin = new SinMathOperator();
            sin.Instantiate(new []{A.Simplyfy()});
            return sin;
        }
        public override IBaseMathOperator Derivate()
        {
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            
            var cos = new CosMathOperator();
            cos.Instantiate(new []{A});
            
            var result = new MultiplyMathOperator();
            result.Instantiate(new []{A.Derivate(), cos});
            return result;
        }
    }
}