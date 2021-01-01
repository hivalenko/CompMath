using System;

namespace Lab3.Models.Methods
{
    public class LagrangeMethod : IInterpolationMethod
    {
        private double[] xData;
        private double[] yData;
        private double step;
        
        public double getY(double[] xData, double[] yData,  double x, double step)
        {
            if (xData == null || yData == null)
                throw new ArgumentNullException();
            if (xData.Length != yData.Length) 
                throw new ArgumentException("У каждой точки должны быть x и y координаты");

            this.xData = xData;
            this.yData = yData;
            this.step = step;
            
            return Pn(x);
        }
        
        double Pn(double x)
        {
            int n = xData.Length;
            double result = Cj(0);
            for (int j = 1; j < n; j++)
            {
                double something = Cj(j);
                for (int k = 0; k < j; k++)
                    something *= x - xData[k];
                result += something;
            }
            return result;
        }

        double Cj(int j)
        {
            if (j == 0)
                return yData[0];
            double delta = Delta(j, j);
            return delta / (Factorial(j) * Math.Pow(step, j));
        }

        double Delta(int p, int i)
        {
            if (p == 1)
                return yData[i] - yData[i - 1];
            return Delta(p - 1, i) - Delta(p - 1, i - 1);
        }

        double Factorial(double value)
        {
            if (value == 0)
                return 1;
            return value * Factorial(value - 1);
        }

    }
}