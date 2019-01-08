
using System;
using System.Text.RegularExpressions;
using Logic.Abstract;
using Logic.interfaces;
using Logic.Operators;
using static System.Double;

namespace Logic
{
    public class StringParser : AbstractParser
    {
        private readonly string _data;


        private IBaseMathOperator _operator;




        public StringParser(string data)
        {
            _data = data.Trim();
            StartParsing();
        }





        private void StartParsing()
        {
            if (IsdataANumberOrX())
            {
                _operator = GetNumberOrXOperator();
                return;
            }
                
            switch (_data[0])
            {
                case '!':
                    _operator = new FactorialMathOperator(GetNumberInOperator());
                    return;
                case 'n':
                case 'r':
                    _operator = new ConstantMathOperator(GetNumberInOperator());
                    return;
                
            }
            
            var factory = new OperatorFactory();
            var oper = factory.GetOperator(_data[0]);
            Console.WriteLine(_data[0]);
            oper.Instantiate(oper.GetOperatorNeededArguments() == 2
                ? new[] {GetFirstArgument(), GetSecondArgument()}
                : new[] {GetFirstArgument()});
            _operator = oper;
        }

        private IBaseMathOperator GetFirstArgument()
        {
            var substring = _data.Substring(1, GetArgumentCommaIndex() - 1).Substring(1).Trim();
            var parser = new StringParser(substring);
            return parser.GetOperator();
        }
        private IBaseMathOperator GetSecondArgument()
        {
            var start = GetArgumentCommaIndex() + 1;
            var substring = _data.Substring(start, _data.Length - 1 - start);
            var parser = new StringParser(substring);
            return parser.GetOperator();
        }

        private int GetArgumentCommaIndex()
        {
            var amountNesting = 0;
            //currentChar is the operator icon (read: -,+,* etc.)
            for (var i = 1; i < _data.Length; i++)
            {
                var temp = _data[i];
                switch (_data[i])
                {
                    case ',' when amountNesting == 1:
                        return i;
                    case '(':
                        amountNesting++;
                        break;
                    case ')':
                        amountNesting--;
                        if (amountNesting < 1)
                        {
                            return i;
                        }
                        break;
                }
            } 
            throw new IndexOutOfRangeException();
        }

        private double GetNumberInOperator()
        {
            var m = Regex.Match(_data, @".\((-?\d*\.?\d+)\)");
            return Parse(m.Groups[1].Value);
        }
        private bool IsdataANumberOrX()
        {
            var m = Regex.Match(_data, @"^[0-9xp]$");
            return m.Success;
        }

        private IBaseMathOperator GetNumberOrXOperator()
        {
            switch (_data[0])
            {
                case 'x':
                    return new VariableXMathOperator();
                case 'p':
                    return new PIMathOperator();
            }
            return new ConstantMathOperator(Parse(_data));
        }


        public override string ToString()
        {
            return _data;
        }

        public override IBaseMathOperator GetOperator()
        {
            return _operator;
        }



    
    }
}