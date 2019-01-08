using System;
using System.Globalization;
using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class ConstantMathOperator : AbstractConstantMathOperator
    {
        public ConstantMathOperator(double d) : base(d)
        {
        }
    }
}
