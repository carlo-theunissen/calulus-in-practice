namespace Logic.interfaces
{
    public interface ISingleMathOperator : IBaseMathOperator
    {
        void Instantiate(IBaseMathOperator[] arguments);
    }
}