using System;
using System.Collections.Generic;

namespace Lab1
{
    public static class Gauss
    {
        public static double[] Solve(ref List<object> matrixInput, int maxMatrixHeight)
        {
            
            Matrix matrix = new Matrix((double[,])matrixInput[0], maxMatrixHeight);
            if (matrix.Width != matrix.Height + 1)
                throw new ArgumentException("System is not suitable to solving by Gauss method.");

            if (!matrix.IsTriangular()) ToTriangular(ref matrix);
            if (!matrix.IsTriangular()) throw new ArgumentException("Can not transfer to triangular");
            CheckZeroOrInfinitySolutions(matrix);
            
            return FindSolutions(matrix);
        }

        public static decimal[] GetErrors(ref List<object> matrixInput, ref List<object> solutionInput, int maxmatrixHeight)
        {
            double[] solution = (double[])solutionInput[0];
            Matrix matrix = new Matrix((double[,])matrixInput[0],maxmatrixHeight);
            return matrix.GetErrors(solution);
        }
        
        private static void CheckZeroOrInfinitySolutions(Matrix matrix)
        {
            bool noSolutions = false;
            bool infinitySolutions = false;
            for (var i = 0; i < matrix.Height; i++)
            {
                var isEquationCorrect = false;
                for (var j = i; j < matrix.Width - 1; j++)
                {
                    if (!NumericComparer.Compare(matrix[i, j], 0)) isEquationCorrect = true;
                }

                if (isEquationCorrect) continue;
                if (NumericComparer.Compare(matrix[i, matrix.Width - 1], 0))
                {
                    infinitySolutions = true;
                }
                else
                {
                    noSolutions = true;
                }
            }
            if (noSolutions) throw new ArgumentException("No solutions");
            if (infinitySolutions) throw new ArgumentException("Infinite number of solutions");
        }
        
        private static double[] FindSolutions(Matrix matrix)
        {
            var height = matrix.Height;
            var width = matrix.Width;
            var solutions = new double[height];
            
            for (var i = height - 1; i >= 0; i--)
            {
                
                var x = matrix[i, width - 1];
                
                for (var j = i + 1; j < width - 1; j++)
                    x -= matrix[i, j] * solutions[j];

                x /= matrix[i, i];
                if (double.IsNaN(x))
                {
                    throw new ArgumentException("Some argument exception enter another matrix");
                }
                solutions[i] = x;
            }

            return solutions;
        }
        
        private static void ToTriangular(ref Matrix matrix)
        {
            for (var i = 0; i < matrix.Height; i++)
            for (var j = i + 1; j < matrix.Height; j++)
                if (!NumericComparer.Compare(matrix[i, i], 0))
                {
                    if (!NumericComparer.Compare(matrix[j, i], 0))
                        matrix.AddRow(i, j, -matrix[j, i] / matrix[i, i]);
                }
                else
                {
                    throw new ArgumentException("Matrix can not be transfered to stairs state");
                }
        }
        
        
    
    }
}