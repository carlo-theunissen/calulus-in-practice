using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class MultiplyMathOperator : AbstractDubbleMathOperator
    {
        public override double Result()
        {
            var a = A.Result();
            var b = B.Result();
            return SimpleCalculation(a,b);
        }


        public override string MathSymbol()
        {
            return "*";
        }

        public override IBaseMathOperator Derivate()
        {
            if (A is ConstantMathOperator && B is ConstantMathOperator)
            {
                return new ConstantMathOperator(0);
            }
            if (!(A is ConstantMathOperator || B is ConstantMathOperator))
            {
                var left = new MultiplyMathOperator();
                left.Instantiate(new []{A.Derivate(), B});
                
                var right = new MultiplyMathOperator();
                right.Instantiate(new []{B.Derivate(), A});
                
                var add = new AddMathOperator();
                add.Instantiate(new IBaseMathOperator[] {left, right});
                
                return add;
            }
            var ADir = A is ConstantMathOperator ? A : A.Derivate();
            var BDir = B is ConstantMathOperator ? B : B.Derivate();
            
            var multi = new MultiplyMathOperator();
            multi.Instantiate(new [] {ADir, BDir});
                
            return multi;
            
        }

        public override IBaseMathOperator CalculateSimplyfy()
        {
            var variable = new VariableXMathOperator();
            switch (A)
            {
                case ConstantMathOperator _ when B is VariableXMathOperator:
                    variable.SetMultiply(((VariableXMathOperator) B).GetMultiply() * A.Result());
                    return variable;
                case VariableXMathOperator @operator when B is ConstantMathOperator:
                    variable.SetMultiply(@operator.GetMultiply() * B.Result());
                    return variable;
            }
            if (A is ConstantMathOperator && Math.Abs(A.Result()) < 0.000001 ||
                B is ConstantMathOperator && Math.Abs(B.Result()) < 0.000001)
            {
                return new ConstantMathOperator(0);
            }

            if (A.Simplyfy() is ConstantMathOperator && Math.Abs(A.Result() - 1) < 0.000001d)
            {
                return B.Simplyfy();
            }
            if (B.Simplyfy() is ConstantMathOperator && Math.Abs(B.Result() - 1) < 0.000001d)
            {
                return A.Simplyfy();
            }
            return base.CalculateSimplyfy();
        }

        public IList<IBaseMathOperator> GetMultiplyList()
        {
            var list = new List<IBaseMathOperator>();
            if (A is MultiplyMathOperator a)
            {
                list = list.Union(a.GetMultiplyList()).ToList();
            }
            else
            {
                list.Add(A);
            }
            if (B is MultiplyMathOperator b)
            {
                list = list.Union(b.GetMultiplyList()).ToList();
            }
            else
            {
                list.Add(B);
            }
           
            return list;
        }

        protected override double SimpleCalculation(double a, double b)
        {
            return Round(a * b);
        }
        protected override ISingleMathOperator CreateEmptyInstance()
        {
            return new MultiplyMathOperator();
        }
        
    }
}