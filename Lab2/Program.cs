using System;
using System.Collections.Generic;
using ConsoleMenu;

namespace Lab2
{
    delegate Tuple<double, double, double, bool> MyFunc<T1>(ref T1 a);
    
    class Program
    {
        static void Main(string[] args)
        {
            IO io = new IO();
            
            List<object> inputResults = new List<object>();
            List<object> computeResults = new List<object>();
            
            Menu sympsonMenu = new MenuConsole("Sympson method", io);
            Menu inputMenu = new MenuConsole("Input menu", io);
            Menu computeMenu = new MenuConsole("Compute menu", io);
            Menu outputMenu = new MenuConsole("Output menu", io);
            
            inputMenu.Add("Input via Console", new Func<Tuple<int, int ,int ,double>>(io.GetFunctionAndParams), null, ref inputResults, true);
            computeMenu.Add("Compute", new MyFunc<List<object>>(MSympson.Solve), new List<object>{inputResults}, ref computeResults, true);
            outputMenu.Add("Print results", new Action<List<object>>(io.Print), new List<object>{computeResults}, false);
            
            sympsonMenu.Add("Sympson method", inputMenu, false);
            sympsonMenu.AddAction(computeMenu, false);
            computeMenu.AddAction(outputMenu, false);
            
            sympsonMenu.Start();
        }
    }
}