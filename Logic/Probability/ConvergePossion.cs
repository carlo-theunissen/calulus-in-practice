namespace Logic.Probability
{
    public class ConvergePossion : INumberGenerator
    {
        private ExponentialGenerator _generator;
        private int _interval;
        private int extend = 0;
        public ConvergePossion(ExponentialGenerator generator, int intervalLength)
        {
            _generator = generator;
            _interval = intervalLength;
        }
        
        public int GetNext()
        {
            var i = 0;
            while (extend <= _interval)
            {
                i++;
                extend += _generator.GetNext();
            }
            extend %= _interval;
            return i;
        }
    }
}