
using System;
using System.Collections.Generic;

namespace Lab2
{
    public class IO : ConsoleMenu.IO
    {
        public Tuple<int, int ,int ,double> GetFunctionAndParams()
        {
            int func = getFunctionNumber();
            int low = getLimit("low");
            int high = getLimit("high");
            double ac = getAccurancy();
            return new Tuple<int, int, int, double>(func, low, high ,ac);
        }

        public void Print(List<object> inputList)
        {
            var input = (Tuple<double, double, double, bool>)inputList[0];
            double rez = input.Item1;
            double p = input.Item2;
            double uac = input.Item3;
            bool flag = input.Item4;
            if (p != 0)
            {
                if (flag == true) rez = rez * (-1);
                PrintMessage("Value of goten integral: " + Math.Round(rez, 9));
                PrintMessage("Amount of steps: " + p);
                PrintMessage("Error: " + Math.Round(uac, 9));
            }
            GetConsoleInput();
        }
        public double getAccurancy()
        {
            string s;
            double ac;
            bool mistake = false;
            do
            {
                PrintMessage("Input accurancy: ");
                s = GetConsoleInput();
                if (!double.TryParse(s, out ac) || ac <= 0) 
                {
                    mistake = true;
                    PrintMessage("Incorrect input, please try again");
                }
                else mistake = false;
            }
            while (mistake);
            return ac;
        }
        public int getFunctionNumber()
        {
            string s;
            int num;
            bool mistake = false;
            do
            {
                PrintMessage("Choose function, integral of which we would like to compute: \n 1. 2*x^2 + 3*x - 1 \n 2. 1 / (1 + x) \n 3. sqrt(1 + x ^2)");
                s = GetConsoleInput();
                if (!int.TryParse(s, out num) || num < 1 || num > 3)
                {
                    mistake = true;
                    PrintMessage("Incorrect input, please try again");
                }
                else mistake = false;
            }
            while (mistake);
            return num;
        }
        
        public int getLimit(string text)
        {
            string s;
            int lim;
            bool mistake = false;
            do
            {
                PrintMessage("Enter " + text + " limit of integration:");
                s = GetConsoleInput();
                if (!int.TryParse(s, out lim))
                {
                    mistake = true;
                    PrintMessage("Incorrect enter, please try again");
                }
                else mistake = false;
            }
            while (mistake);
            return lim;
        }
    }
}