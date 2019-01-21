using System;

namespace Lab1
{
    public class NumericComparer
    {
        public static double Tolerance = 1E-7;

        public static bool Compare(double x, double y) => Math.Abs(x - y) < Tolerance;
    }
}