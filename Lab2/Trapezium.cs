using System;
using System.Collections.Generic;

namespace Lab2
{

    public delegate double Function(double x);
    
    public static class Trapezium
    {
        private const int INITIAL_STEP_AMOUNT = 100;
        
        private static double CountArea(double base1, double base2, double height)
        {
            return (base1 + base2)/2 * height;
        }

        private static double GetApproximpateIntegral(Function function, double a, double b, double step)
        {
            double x, result = 0;

            for (x = a; x <= b; x += step)
                result += CountArea(function(x), function(x + step), step);
            
            if(double.IsInfinity(result) || double.IsNaN(result))
                throw new ArgumentException("Function is not defined in this input.");

            return result;
        }

        public static Tuple<double, int, double> Solve(ref List<object> inputs)
        {
            Tuple<Function, double, double, double> input = (Tuple<Function, double, double, double>) inputs[0];
            Function function = input.Item1;
            double a = input.Item2;
            double b = input.Item3;
            double prcision = input.Item4;
            double error = prcision;

            if (a == b) return Tuple.Create(0d, 0, 0d);

            if (a > b)
            {
                double temp = b;
                b = a;
                a = temp;
            }

            if (double.IsInfinity(function(a)) || double.IsNaN(function(a)))
                a += double.Epsilon;
            if (double.IsInfinity(function(b)) || double.IsNaN(function(b)))
                b -= double.Epsilon;

            double step = (b - a) / INITIAL_STEP_AMOUNT;
            double integral_n;

            do
            {
                integral_n = GetApproximpateIntegral(function, a, b, step);
                double integral_2n = GetApproximpateIntegral(function, a, b, step/2);
                
                error = 1f/3 * Math.Abs(integral_2n - integral_n);

                if (error >= prcision) step /= 2;
                
            } while (error >= prcision);
            
            Tuple<double, int, double> result = Tuple.Create(integral_n, (int) ((b - a) / step), error);
            
            return result;
        }
    }
    
}