using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class DevideMathOperator : AbstractDubbleMathOperator
    {
        public override IBaseMathOperator CalculateSimplyfy()
        {
            
            if (A.Simplyfy().ToMathString().Equals(B.Simplyfy().ToMathString()))
            {
                return new ConstantMathOperator(1);
            }
            if (A.Simplyfy() is AbstractConstantMathOperator && Math.Abs(A.Simplyfy().Result()) < 0.0001d)
            {
                return new ConstantMathOperator(0);
            }
            if (A.Simplyfy() is MultiplyMathOperator && B.Simplyfy() is MultiplyMathOperator b)
            {
                var alist = ((MultiplyMathOperator ) A.Simplyfy()).GetMultiplyList().ToList();
                var blist = ((MultiplyMathOperator ) B.Simplyfy()).GetMultiplyList().ToList();

                var duplicates = alist.Where(x => blist.Any(y => y.ToMathString().Equals(x.ToMathString())));
                
                var newAList = alist.Where(x => !duplicates.Any(y => y.ToMathString().Equals(x.ToMathString())));
                var newBList = blist.Where(x => !duplicates.Any(y => y.ToMathString().Equals(x.ToMathString())));

                if (newAList.Any() && newBList.Any())
                {
                    var divide = new DevideMathOperator();
                    divide.Instantiate(new[]
                    {
                        new MultiplyMathOperator().CreateFromList(newAList.ToList()),
                        new MultiplyMathOperator().CreateFromList(newBList.ToList())
                    });
                    return divide;
                }
                else
                {
                    if (newAList.Any())
                    {
                        return new MultiplyMathOperator().CreateFromList(newAList.ToList());
                    }
                    var divide = new DevideMathOperator();
                    divide.Instantiate(new[]
                    {
                        new ConstantMathOperator(1), 
                        new MultiplyMathOperator().CreateFromList(newBList.ToList())
                    });
                    return divide;
                }
            }
            
            var instance = CreateEmptyInstance();
            instance.Instantiate(new []{A.Simplyfy(), B.Simplyfy()});
            return instance;
        }

        public override double Result()
        {
            return SimpleCalculation(A.Result(), B.Result());
        }

        public override string MathSymbol()
        {
            return "/";
        }


        public override IBaseMathOperator Derivate()
        {
            if (IsConstant())
            {
                return new ConstantMathOperator(0);
            }
            
            var topFirstMultiply = new MultiplyMathOperator();
            topFirstMultiply.Instantiate(new []{B, A.Derivate()});
            
            var topSecondMultiply = new MultiplyMathOperator();
            topSecondMultiply.Instantiate(new []{A, B.Derivate()});
            
            var topMin = new MinMathOperator();
            topMin.Instantiate(new IBaseMathOperator[]{topFirstMultiply, topSecondMultiply});

            var bottom = ExpotentialOperator.SetExpotentialOperator(B, 2);
            
            var divide = new DevideMathOperator();
            divide.Instantiate(new IBaseMathOperator[]{topMin, bottom});
            return divide;
        }

        protected override double SimpleCalculation(double a, double b)
        {
            return Round(a / b);
        }
        protected override ISingleMathOperator CreateEmptyInstance()
        {
            return new DevideMathOperator();
        }
    }
}