using System;
using System.Globalization;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractConstantMathOperator : AbstractBaseMathOperator
    {
        protected double data;

        public AbstractConstantMathOperator(double d)
        {
            this.data = d;
        }

        public override double Result()
        {
            return data;
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
            return MathSymbol();
        }

        public override string MathSymbol()
        {
            return Result().ToString(CultureInfo.InvariantCulture);
        }

        public override IBaseMathOperator Simplyfy()
        {
            return this;
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            return this;
        }

        public override IBaseMathOperator Derivate()
        {
            return new ConstantMathOperator(0); //derivate of a constant is nothing
        }
    }
}