using System;
using System.Collections.Generic;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractDubbleMathOperator : AbstractSingleMathOperator, IDubbleMathOperator
    {
        protected IBaseMathOperator B;
        public override void Instantiate(IBaseMathOperator[] arguments)
        {
            base.Instantiate(arguments);
            B = arguments[1];
        }
        public override int GetOperatorNeededArguments()
        {
            return 2;
        }
        
        public override void SetXValue(double number)
        {
            base.SetXValue(number);
            B.SetXValue(number);
        }
        
        public override IBaseMathOperator[] GetChilds()
        {
            return new[] {A, B};
        }
        
        public override string ToMathString()
        {
            return "(" + A.ToMathString() + " " + MathSymbol() + " " + B.ToMathString() + ")";
        }

        protected override bool IsConstant()
        {
            return base.IsConstant() && B is ConstantMathOperator;
        }

        protected abstract double SimpleCalculation(double a, double b);
        protected abstract ISingleMathOperator CreateEmptyInstance();
        public override IBaseMathOperator CalculateSimplyfy()
        {
            if (A.Simplyfy() is ConstantMathOperator && B.Simplyfy() is ConstantMathOperator)
            {
                var result = SimpleCalculation(A.Simplyfy().Result(), B.Simplyfy().Result());
                if (Math.Abs(Math.Round(result) - result) < 0.000001d)
                {
                    return new ConstantMathOperator(result);
                }
            }

            if (A.Simplyfy() is VariableXMathOperator && B.Simplyfy() is VariableXMathOperator)
            {
                var variable = new VariableXMathOperator();
                variable.SetMultiply(SimpleCalculation((A.Simplyfy() as VariableXMathOperator).GetMultiply() , (B.Simplyfy() as VariableXMathOperator).GetMultiply()));
                return variable;
            }

            var instance = CreateEmptyInstance();
            instance.Instantiate(new []{A.Simplyfy(), B.Simplyfy()});
            return instance;
        }
        
        public IBaseMathOperator CreateFromList(List<IBaseMathOperator> operators)
        {
            if (operators.Count == 1)
            {
                return operators[0];
            }
            
            var inst = CreateEmptyInstance();
            var now = operators[0];
            operators.RemoveAt(0);
            var second = operators.Count > 1 ? CreateFromList(operators) : operators[0];
            inst.Instantiate(new []{now, second});
            return inst;
        }
    }
}