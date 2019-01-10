using System;
using System.Collections;
using System.Collections.Generic;

namespace Logic.Probability
{
    public class BasicGraph : IGraphValueProvider
    {
        private INumberGenerator _generator;
        private int _steps;
        private int[] _numbers;
        public BasicGraph(INumberGenerator generator, int steps)
        {
            _generator = generator;
            _steps = steps;
            
        }
        
        public int[] GetSteps()
        {
            _numbers = new int[0];
            for (var i = 0; i < _steps; i++)
            {
                var work = _generator.GetNext();
                AddToList(work);
            }
            return _numbers;
        }

        private void AddToList(int number)
        {
            if (_numbers.Length < number+1)
            {
                var newArray = new int[number+1];
                _numbers.CopyTo(newArray,0);
                _numbers = newArray;
            }
            _numbers[number]++;
        }
    }
}