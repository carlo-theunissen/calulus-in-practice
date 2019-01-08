using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class EMathOperator : ConstantMathOperator
    {
        public EMathOperator() : base(Math.E){}
        public override string MathSymbol()
        {
            return "e";
        }
    }
}