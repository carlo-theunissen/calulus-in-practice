using System;

namespace Logic.Probability
{
    public class ExponentialGenerator : INumberGenerator
    {
        private readonly double _lambda;
        private int _scale;
        public ExponentialGenerator(double lambda, int scale)
        {
            _scale = scale;
            _lambda = lambda;
        }

        public int GetNext()
        {
            double next  = new Random().NextDouble();
            var result =  Math.Round((Math.Log(1 - next) / (-1 * _lambda)) * _scale);
            return (int) result;
        }
    }
}