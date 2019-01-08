namespace Logic.Probability
{
    public interface IGraphValueProvider
    {
        int GetSteps();
        int GetStepValue(int x);
    }
}