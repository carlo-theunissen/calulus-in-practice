using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class LogMathOperator : AbstractSingleMathOperator
    {

        public override double Result()
        {
            var calc = Math.Log(A.Result());
            return Round(calc);
        }

        public override string ToMathString()
        {
            return "log("+A.ToMathString()+")" ;
        }

        public override string MathSymbol()
        {
            return "log";
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            var log = new LogMathOperator();
            log.Instantiate(new []{A.Simplyfy()});
            return log;
        }

        public override IBaseMathOperator Derivate()
        {
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            var divide = new DevideMathOperator();
            divide.Instantiate(new []{ new ConstantMathOperator(1), A });
            return divide;
        }
    }
}