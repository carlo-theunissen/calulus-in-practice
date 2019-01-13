using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class PIMathOperator : AbstractConstantMathOperator
    {
        public PIMathOperator() : base(Math.PI){}
        public override string MathSymbol()
        {
            return "PI";
        }
    }
}