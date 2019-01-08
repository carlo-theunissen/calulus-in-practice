using System;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    internal class ExpotentialOperator : AbstractDubbleMathOperator
    {
        public override double Result()
        {
            return SimpleCalculation(A.Result(), B.Result());
        }

        public override string MathSymbol()
        {
            return "^";
        }

        public override IBaseMathOperator Derivate()
        {
            var newThis = new ExpotentialOperator();
            newThis.Instantiate(GetChilds());
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            
            if (B is ConstantMathOperator)
            {
                var times = new MultiplyMathOperator();
                var constant = new ConstantMathOperator(B.Result());
                var expo = new ExpotentialOperator();
                expo.Instantiate( new []{ A, new ConstantMathOperator(B.Result() - 1)});
                
                times.Instantiate(new IBaseMathOperator[] {constant, expo});
                return times;
            }
            
            var log = new LogMathOperator();
            log.Instantiate(new[] {A});

            if (A is ConstantMathOperator)
            {
                //chain rule
                var leftTimes = new MultiplyMathOperator();
                leftTimes.Instantiate(new[] {A, B.Derivate()});

                var expo2 = new ExpotentialOperator();
                expo2.Instantiate(new[] {leftTimes, B});

                //end..

                var multi = new MultiplyMathOperator();
                multi.Instantiate(new IBaseMathOperator[] {expo2, log});
                return multi;
            }
            
            var rightMulti = new MultiplyMathOperator();
            rightMulti.Instantiate(new []{log, B});
            
            var result = new MultiplyMathOperator();
            result.Instantiate(new []{newThis, rightMulti.Derivate()});
            return result;

        }


        public static ExpotentialOperator SetExpotentialOperator(IBaseMathOperator A, float number)
        {
            var expo = new ExpotentialOperator();
            if (A is ExpotentialOperator)
            {
                var times = new MultiplyMathOperator();
                times.Instantiate(new IBaseMathOperator[]{A.GetChilds()[1], new ConstantMathOperator(number), });
                expo.Instantiate(new IBaseMathOperator[]{A.GetChilds()[0], times});
                return expo;
            }
            
            expo.Instantiate(new IBaseMathOperator[]{A , new ConstantMathOperator(number)});
            return expo;
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            switch (B)
            {
                case ConstantMathOperator _ when Math.Abs(B.Result()) < 0.000001:
                    return new ConstantMathOperator(1);
                case ConstantMathOperator _ when Math.Abs(B.Result() - 1) < 0.000001:
                    return A.Simplyfy();
            }

            var expo = new ExpotentialOperator();
            expo.Instantiate(new []{A.Simplyfy(), B.Simplyfy()});
            return expo;
        }
        protected override ISingleMathOperator CreateEmptyInstance()
        {
            return new ExpotentialOperator();
        }

        protected override double SimpleCalculation(double a, double b)
        {
            var calc =  Round(Math.Pow(a , b));
            return calc;
        }
    }
}