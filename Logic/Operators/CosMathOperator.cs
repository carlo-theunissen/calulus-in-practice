using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class CosMathOperator : AbstractSingleMathOperator
    {

        public override double Result()
        {
            return Round(Math.Cos(A.Result()));
        }

        public override string ToMathString()
        {
            return "cos("+A.ToMathString()+")" ;
        }

        public override string MathSymbol()
        {
            return "cos";
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            var cos = new CosMathOperator();
            cos.Instantiate(new []{A.Simplyfy()});
            return cos;
        }

        public override IBaseMathOperator Derivate()
        {
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            
            var sin = new SinMathOperator();
            sin.Instantiate(new []{A});
            var multiply = new MultiplyMathOperator();
            multiply.Instantiate(new IBaseMathOperator[]{new ConstantMathOperator(-1), sin });
            
            var result = new MultiplyMathOperator();
            result.Instantiate(new []{A.Derivate(), multiply});
            return result;
        }
    }
}