using System;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class MinMathOperator : AbstractDubbleMathOperator
    {
        public override double Result()
        {
            return A.Result() - B.Result();
        }


        public override string MathSymbol()
        {
            return "-";
        }

        public override IBaseMathOperator Derivate()
        {
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            
            var min = new MinMathOperator
            {
                A = A.Derivate(),
                B = B.Derivate()
            };
            return min;
        }


        public override IBaseMathOperator CalculateSimplyfy()
        {
            if (A is ConstantMathOperator && Math.Abs(A.Result()) < 0.000001 ||
                B is ConstantMathOperator && Math.Abs(B.Result()) < 0.000001)
            {
                return A is ConstantMathOperator ? B.Simplyfy() : A.Simplyfy();
            }
            return base.CalculateSimplyfy();
        }
        protected override double SimpleCalculation(double a, double b)
        {
            return Round(a - b);
        }
        protected override ISingleMathOperator CreateEmptyInstance()
        {
            return new MinMathOperator();
        }

    }
}