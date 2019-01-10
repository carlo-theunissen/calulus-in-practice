using System;

namespace Logic.Probability
{
    public class PosionGenerator : INumberGenerator
    {
        private int _tries;
        private int _lambda;
        private UniformGenerator _generator;
        public PosionGenerator(int tries, double lambda)
        {
            _tries = tries;
            _lambda = (int) Math.Round(lambda * 100);
            _generator = new UniformGenerator(0, 1000);
        }

        public int GetNext()
        {
            var success = 0;
            for (var i = 0; i < _tries; i++)
            {
                if (_generator.GetNext() < _lambda)
                {
                    success++;
                }
            }
            return success;
        }
        
    }
}