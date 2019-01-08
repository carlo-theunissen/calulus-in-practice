namespace Logic.interfaces
{


    public interface IBaseMathOperator
    {
        double Result();
        void SetXValue(double number);
        IBaseMathOperator[] GetChilds();
        int GetOperatorNeededArguments();
        string ToMathString();
        string MathSymbol();
        IBaseMathOperator Simplyfy();
        IBaseMathOperator DeepSimplyfy();
        IBaseMathOperator Derivate();
    }
}