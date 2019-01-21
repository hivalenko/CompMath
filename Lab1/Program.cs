using System;
using System.Collections.Generic;
using ConsoleMenu;

namespace Lab1
{
    delegate double[,] MyFunc<T1>(ref T1 a);

    delegate double[] MyFunc<T1, T2>(ref T1 a, T2 b);

    delegate decimal[] MyFunc<T1, T2, T3>(ref T1 a, ref T2 b, T3 c);


    class Program
    {
        static void Main(string[] args)
        {
            int maxMatrixHeight = 20;
            int generatedMatrixMaxSize = 20;
            const int generatedMatrixMinSize = 0;
            IO io = new IO();
            
            List<object> input = new List<object>();
            List<object> matrixResutlts = new List<object>();
            
            Menu mainMenu = new MenuConsole("Gauss programm", io);
            Menu inputMenu = new MenuConsole("Choose your input", io);
            Menu computeMenu = new MenuConsole("What to do next", io);
            Menu resultsMenu = new MenuConsole("Choose info you are interested in", io);
           
            inputMenu.Add("Input from console", new Func<int, double[,]>(io.ConsoleMatrixInput), new List<object>{maxMatrixHeight}, ref input, true);
            inputMenu.AddAction(new Action<List<object>, int>(io.PrintMatrix), new List<object>{input, maxMatrixHeight}, false );
            inputMenu.Add("Input from file", new Func<int, double[,]>(io.FileMatrixInput), new List<object>{maxMatrixHeight}, ref input, true);
            inputMenu.AddAction(new Action<List<object>, int>(io.PrintMatrix), new List<object>{input, maxMatrixHeight}, false );
            inputMenu.Add("Random matrix", new Func<int, int, int, double[,]>(io.GenerateMatrix), new List<object>{maxMatrixHeight, generatedMatrixMaxSize, generatedMatrixMinSize}, ref input, true);
            inputMenu.AddAction(new Action<List<object>, int>(io.PrintMatrix), new List<object>{input, maxMatrixHeight}, false );

            computeMenu.Add("Solve", new MyFunc<List<object>, int>(Gauss.Solve), new List<object>{input, maxMatrixHeight}, ref matrixResutlts, false);
            computeMenu.AddAction(new MyFunc<List<object>,List<object>, int>(Gauss.GetErrors), new List<object>{input, matrixResutlts, maxMatrixHeight}, false);
            
            
            resultsMenu.Add("Print solutions and nevyazki", new Action<List<object>>(io.PrintSolutionsAndNevyaz), new List<object>{matrixResutlts}, false);
            resultsMenu.Add("Print triangular", new Action<List<object>, int>(io.PrintTriangular), new List<object>{input, maxMatrixHeight}, false);
            resultsMenu.Add("Print determenant", new Action<List<object>, int>(io.PrintDetermenant), new List<object>{input, maxMatrixHeight}, false);
            resultsMenu.Add("Print whole solution", new Action<List<object>, List<object>, int>(io.PrintWholeSolution), new List<object>{input, matrixResutlts, maxMatrixHeight}, false);
            
            
            computeMenu.AddAction(resultsMenu, false);

            mainMenu.Add("Choose input methods", inputMenu, false);
            mainMenu.AddAction(computeMenu, false);
            
            mainMenu.Start();

        }
    }
}