using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1
{
    public class IO: ConsoleMenu.IO
    {
        public void PrintMatrix(List<object> m, int maxMatrixHeight)
        {
            Matrix matrix = new Matrix((double[,]) m[0], maxMatrixHeight);
            PrintMessage("Input matrix");
            PrintMessage(matrix.ToString());
            GetEmptyEnter();
        }
        
        public void PrintTriangularMatrix(Matrix matrix)
        {
            PrintMessage("Triangular");
            PrintMessage(matrix.ToString());
            GetEmptyEnter();
        }
        
        public int AskCount(int maxMatrixHeight)
        {
            do
            {
                PrintMessage("Enter size of matrix k");
                var isNumber = int.TryParse(Console.ReadLine(), out var k);
                if (!isNumber)
                {
                    PrintMessage("Input is not rational");
                }
                else
                {
                    if (k > 0 && k <= maxMatrixHeight) return k;
                    PrintMessage($"K should be greater than 0 and less than {maxMatrixHeight}.");
                }
            } while (true);
        }

        public void PrintSolutionsAndNevyazMatrix(double[] solutions, decimal[] errors)
        {
            PrintMessage("Solutions and nevyazki");
            for (var i = 0; i < solutions.Length; i++)
            {
                PrintMessage($"X{i+1}: {solutions[i]:E3} | error: {errors[i]:E3}");
            }

            GetConsoleInput();
        }

        public void PrintDetermenantMatrix(Matrix matrix)
        {
            PrintMessage("Determinant of matrix");
            PrintMessage(matrix.GetDeterminant().ToString("E3"));
            GetConsoleInput();
        }

        public void PrintWholeSolution(List<object> matrixEs, List<object> solutionsErrors, int maxMatrixHeight)
        {
            Matrix matrix = new Matrix((double[,])matrixEs[0], maxMatrixHeight);
            double[] solutions = (double[])solutionsErrors[0];
            decimal[] errors = (decimal[])solutionsErrors[1];
            PrintTriangularMatrix(matrix);
            PrintSolutionsAndNevyazMatrix(solutions, errors);
            PrintDetermenantMatrix(matrix);
        }

        public void PrintSolutionsAndNevyaz( List<object> solutionsErrors)
        {
            double[] solutions = (double[])solutionsErrors[0];
            decimal[] errors = (decimal[])solutionsErrors[1];
            PrintSolutionsAndNevyazMatrix(solutions, errors);
            GetConsoleInput();
        }
        
        public void PrintDetermenant(List<object> matrixEs, int maxMatrixHeight)
        {
            Matrix matrix = new Matrix((double[,])matrixEs[0], maxMatrixHeight);
            PrintDetermenantMatrix(matrix);
        }

        public void PrintTriangular(List<object> matrixEs, int maxMatrixHeight)
        {
            Matrix matrix = new Matrix((double[,])matrixEs[0], maxMatrixHeight);
            PrintTriangularMatrix(matrix);
        }

        public double[,] FileMatrixInput(int maxMatrixHeight)
        {
            int k = AskCount(maxMatrixHeight);
            PrintMessage("Enter file path");
            var fileName = GetConsoleInput();
            if (fileName == null || !File.Exists(fileName))
            {
                throw new ArgumentException("File does not exit");          
            }

            using (var sr = new StreamReader(fileName))
            {
                var source = sr.ReadToEnd();
                return ManualMatrixParse(source, k);
            }
        } 
        
        public double[,] ConsoleMatrixInput(int maxMatrixHeight)
        {
            int k = AskCount(maxMatrixHeight);
            var matrixString = new StringBuilder();
            for (var i = 0; i < k; i++)
            {
                PrintMessage($"Enter coefficients of equation №{i+1} ");
                matrixString.Append(GetConsoleInput());
                matrixString.Append(' ');
            }

            return ManualMatrixParse(matrixString.ToString(), k);
        }

        public double[,] GenerateMatrix(int maxMatrixHeight, int min, int max)
        {    
            int size = AskCount(maxMatrixHeight);
            var gen = new Random();           
            var height = size;
            var width = size + 1;
            var randomMatrix = new double[height, width];
            var interval = max - min;
            
            for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
                randomMatrix[i, j] = gen.NextDouble() * interval + min;
            
            return randomMatrix;
        }
        
        private double[,] ManualMatrixParse(string source, int k)
        {
            double[,] result;
            result = Matrix.Parse(source, k);
            return result;
        }
    }
}