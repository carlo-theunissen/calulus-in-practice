using System;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractBaseMathOperator : IBaseMathOperator
    {
        public abstract double Result();
        public abstract void SetXValue(double number);
        public abstract IBaseMathOperator[] GetChilds();
        public abstract int GetOperatorNeededArguments();
        public abstract string ToMathString();
        public abstract string MathSymbol();

        public IBaseMathOperator DeepSimplyfy()
        {
            return Simplyfy().Simplyfy();
        }
        public abstract IBaseMathOperator Simplyfy();
        public abstract IBaseMathOperator CalculateSimplyfy();

        public abstract IBaseMathOperator Derivate();

        protected double Round(double number)
        {
            return Math.Round(number * 100000000)/ 100000000;
        }
    }
}