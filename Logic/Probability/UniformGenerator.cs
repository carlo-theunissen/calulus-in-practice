using System;

namespace Logic.Probability
{
    public class UniformGenerator : INumberGenerator
    {
        private Random random;

        private readonly int _start;
        private readonly int _end;
        public UniformGenerator(int start = 0, int end = 1)
        {
            random = new Random();
            _start = start;
            _end = end;
        }
        public int GetNext()
        {
            return random.Next() * (_end - _start) + _start;
        }
    }
}