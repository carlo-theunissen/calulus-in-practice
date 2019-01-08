using System;

namespace Logic.Probability
{
    public class ExponentialGenerator : INumberGenerator
    {
        private readonly int _lambda;
        private readonly UniformGenerator _generator;

        public ExponentialGenerator(int lambda)
        {
            _lambda = lambda * 100;
            _generator = new UniformGenerator(0, 100);
        }

        public int GetNext()
        {
            return (int) Math.Round(Math.Log(1 - _generator.GetNext() / 100d) / (-1 * _lambda));

        }
    }
}