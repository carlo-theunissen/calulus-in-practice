using Logic.interfaces;

namespace Logic.Calculators
{
    public class RiemannSum
    {
        public static double CalculateSum(IBaseMathOperator oper, double start, double end, double increase = 0.00001d)
        {

            double total = 0;
            for (var i = start; i < end; i += increase)
            {
                var result = oper.Result();
                oper.SetXValue(i);
                total += result * increase;
            }
            return total;
        }
    }
}