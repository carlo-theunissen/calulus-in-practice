using System;
using System.Collections.Generic;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Calculators
{
    public class GausJordan
    {
        public static IBaseMathOperator GetBaseMathOperatorFromList(IList<Point> points)
        {                
            return GetBaseMathOperatorFromValues(GetValues(GetMatrixFromPoints(points), GetAnwsersFromPoints(points)));
        }
        
        public static IBaseMathOperator GetBaseMathOperatorFromValues(double[] values)
        {
            var list = new List<IBaseMathOperator>();
            for (var i = 0; i < values.Length; i++)
            {
                var power = new ExpotentialOperator();
                power.Instantiate(new IBaseMathOperator[]{new VariableXMathOperator(), new ConstantMathOperator(values.Length - i - 1) });
                var times = new MultiplyMathOperator();
                times.Instantiate(new IBaseMathOperator[]{ new ConstantMathOperator(values[i]), power });
                list.Add(times);
            }
            return new AddMathOperator().CreateFromList(list);
        }
        
        public static double[] GetAnwsersFromPoints(IList<Point> points)
        {
            return points.Select(x => x.Y).ToArray();
        }
        
        public static double[][] GetMatrixFromPoints(IList<Point> points)
        {
            var result = new double[points.Count][];
            for (var i = 0; i < points.Count; i++)
            {
                result[i] = CalulcateRow(points[i].X, points.Count);
            }
            return result;
        }

        private static double[] CalulcateRow(double x, int rowSize)
        {
            var result = new double[rowSize];
            for (var i = 0; i < rowSize; i++)
            {
                result[i] = Math.Pow(x, rowSize - i - 1);
            }
            return result;
        }
        public static double[] GetValues(double[][] matrix, double[] answers)
        {
            for (var i = 0; i < answers.Length; i++)
            {
                SwapRowUntilNotZero(i, i, matrix, answers);
                for (var j = 0; j < i; j++)
                {
                    if (Math.Abs(matrix[i][j]) > 0.000001d)
                    {
                        answers[i] = answers[i] - answers[j] * matrix[i][j];
                        matrix[i] = SubstractRow(matrix[i], matrix[j], matrix[i][j]);
                    }
                }
                if (Math.Abs(matrix[i][i] - 1) > 0.00001d && Math.Abs(matrix[i][i]) > 0.00001d)
                {
                    var devide = matrix[i][i];
                    answers[i] = answers[i] / devide;
                    matrix[i] = DevideRow(matrix[i], devide);
                }
            }

            for (var i = answers.Length - 2; i >= 0; i--)
            {
                for (var j = i+1; j < answers.Length; j++)
                {
                    answers[i] = answers[i] - answers[j] * matrix[i][j];
                    matrix[i] = SubstractRow(matrix[i], matrix[j], matrix[i][j]);
                }
            }
            return answers;
        }

        private static void SwapRowUntilNotZero(int rowId, int columnId, double[][] matrix, double[] answers)
        {
            var start = rowId + 1;
            while (Math.Abs(matrix[rowId][columnId]) < 0.0001d)
            {
                if (start >= matrix.Length)
                {
                    throw new System.Exception("Not possible");
                }
                var temp = matrix[rowId];
                matrix[rowId] = matrix[start];
                matrix[start] = temp;

                var ansTemp = answers[rowId];
                answers[rowId] = answers[start];
                answers[start] = ansTemp;
                
                start++;
            }
        }

        private static double[] DevideRow(double[] row, double amount)
        {
            var copy = new double[row.Length];
            row.CopyTo(copy, 0);
            for (var i = 0; i < row.Length; i++)
            {
                copy[i] = row[i] / amount;
            }
            return copy;
        }
        
        private static double[] SubstractRow(double[] current, double[] subtract, double amount)
        {
            var copy = new double[current.Length];
            current.CopyTo(copy, 0);
            for (var i = 0; i < current.Length; i++)
            {
                copy[i] = current[i] - amount * subtract[i];
            }
            return copy;
        }
    }
}