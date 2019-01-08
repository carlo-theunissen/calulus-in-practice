using Logic.interfaces;
using Logic.Operators;

namespace Logic
{
    public class OperatorFactory
    {
        public ISingleMathOperator GetOperator(char symbol)
        {
            switch (symbol)
            {
                case '-':
                    return new MinMathOperator();
                case '+':
                    return new AddMathOperator();
                case '*':
                    return new MultiplyMathOperator();
                case '/':
                    return new DevideMathOperator();
                case '^':
                    return new ExpotentialOperator();
                case 'e':
                    return new ExpotentialEOperator();
                case 's':
                    return new SinMathOperator();
                case 'c':
                    return new CosMathOperator();
                case 'l':
                    return new LogMathOperator();
            }
            return null;
        }
    }
}