using System;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    internal class ExpotentialEOperator : AbstractSingleMathOperator
    {


        public override double Result()
        {
            return SimpleCalculation(A.Result());
        }


        public override string ToMathString()
        {
            return "e^("+A.ToMathString()+")";
        }

        public override string MathSymbol()
        {
            return "e^";
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            switch (A)
            {
                case ConstantMathOperator _ when Math.Abs(A.Result() - 1) < 0.000001:
                    return new EMathOperator();
                case ConstantMathOperator _ when Math.Abs(A.Result()) < 0.000001:
                    return new ConstantMathOperator(1);
            }
            var instance = new ExpotentialEOperator();
            instance.Instantiate(new []{A.Simplyfy()});
            return instance;
        }

        public override IBaseMathOperator Derivate()
        {
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            
            var newThis = new ExpotentialEOperator();
            newThis.Instantiate(GetChilds());
            var rightMulti = new MultiplyMathOperator();
            rightMulti.Instantiate(new []{A.Derivate(), newThis});
            
            return rightMulti;
        }
        protected  double SimpleCalculation(double a)
        {
            var calc =  Round(Math.Pow(Math.E , a));
            return  calc;
        }
    }
}