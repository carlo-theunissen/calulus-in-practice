namespace Logic.interfaces
{
    public interface IDubbleMathOperator : ISingleMathOperator
    {
        void Instantiate(IBaseMathOperator[] arguments);
    }
}