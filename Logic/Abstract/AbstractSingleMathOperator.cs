using System;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractSingleMathOperator : AbstractBaseMathOperator, ISingleMathOperator
    {
        protected IBaseMathOperator A;
        private IBaseMathOperator _cachedSimplify = null;
        public virtual void Instantiate(IBaseMathOperator[] arguments)
        {
            A = arguments[0];
        }
        public override IBaseMathOperator[] GetChilds()
        {
            return new []{A};
        }

        public override int GetOperatorNeededArguments()
        {
            return 1;
        }
        public override void SetXValue(double number)
        {
            A.SetXValue(number);
        }

        protected virtual bool IsConstant()
        {
            return A is ConstantMathOperator;
        }

        public override IBaseMathOperator Simplyfy()
        {
            if (_cachedSimplify != null)
            {
                return _cachedSimplify;
            }
            IBaseMathOperator result = null;
            do
            {
                var calc = CalculateSimplyfy();
                if (result != null && calc.ToMathString().Equals(result.ToMathString()))
                {
                    _cachedSimplify = calc;
                    return calc;
                }
                result = calc;

            } while (true);
        }
    }
}