
using System;
using System.Collections.Generic;

namespace Lab2
{
    public class IO : ConsoleMenu.IO
    {
        public Tuple<Function, double, double, double> GetFunctionAndParams()
        {
            Function func = getFunctionNumber();
            double low = getLimit("low");
            double high = getLimit("high");
            double ac = getAccurancy();
            return new Tuple<Function, double, double, double>(func, low, high ,ac);
        }

        public void Print(List<object> inputList)
        {
            var input = (Tuple<double, int, double>)inputList[0];
            
            double IntValue = input.Item1;
            double amountOfDivisions = input.Item2;
            double error = input.Item3;
           
            PrintMessage("Value of goten integral: " + Math.Round(IntValue, 9));
            PrintMessage("Amount of steps: " + amountOfDivisions);
            PrintMessage("Error: " + Math.Round(error, 9));
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
        public Function getFunctionNumber()
        {
            string s;
            int num;
            bool mistake = false;
            do
            {
                PrintMessage("Choose function, integral of which we would like to compute: \n 1. x^2 \n 2. 1 / x \n 3. sqrt(x)");
                s = GetConsoleInput();
                if (!int.TryParse(s, out num) || num < 1 || num > 3)
                {
                    mistake = true;
                    PrintMessage("Incorrect input, please try again");
                }
                else mistake = false;
            }
            while (mistake);

            Function function = x => 0;
            
            switch (num)
            {
                case 1:
                    function = x => x * x;
                    break;
                case 2:
                    function = x => 1 / x;
                    break;
                case 3:
                    function = x => Math.Sqrt(x);
                    break;
            }
            return function;
        }
        
        public double getLimit(string text)
        {
            string s;
            double lim;
            bool mistake = false;
            do
            {
                PrintMessage("Enter " + text + " limit of integration:");
                s = GetConsoleInput();
                if (!double.TryParse(s, out lim))
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