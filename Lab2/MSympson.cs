using System;
using System.Collections.Generic;

namespace Lab2
{
    public static class MSympson
    {
        public static Tuple<double, double, double, bool> Solve(ref List<object> inputList)
        {
            double In, I2n, h, uac, rez, p, high, low, ac;
            Tuple<int, int, int, double> input = (Tuple<int, int, int, double>) inputList[0];
            int f = input.Item1;
            high = input.Item2;
            low = input.Item3;
            ac = input.Item4;
            rez = 0;
            p = 0;
            uac = 0;
            
            bool flag = false;

            if (high < low)
            {
                flag = true;
                double tmp = low;
                low = high;
                high = tmp;
            }
            
            if (high != low)
                {
                for (int n = 4; n <= 10000; n += 2)
                {
                
                    double sum1 = 0, sum2 = 0;
                    h = (high - low) / n; //вычисление размера шага
                    for (int i = 1; i < n; i++)
                    {
                        sum1 += 4 * Point(low + i * h, f);
                        ++i;
                        sum1 += 2 * Point(low + i * h, f);
                    }
                    In = (sum1 + Point(low, f) - Point(high, f)) * h / 3; // вычисление интеграла с количеством шагов = n 
                    h = (high - low) / (2 * n); // увеличение количества шагов в 2 раза
                    for (int i = 1; i < 2 * n; i++)
                    {
                        sum2 += 4 * Point(low + i * h, f);
                        ++i;
                        sum2 += 2 * Point(low + i * h, f);
                    }
                    I2n = (sum2 + Point(low, f) - Point(high, f)) * h / 3;// вычисление интеграла с количеством шагов = 2n 
                    if ((Math.Abs(I2n - In)/15) < ac)  //проверка, заданная пользователем точность меньше вычисленной погрешности? 
                    { 
                        rez = I2n; 
                        p = n; 
                        uac = Math.Abs(I2n - In) / 15; 
                        break; 
                    }
                    if (n == 10000) { throw new ArgumentException("Заданная точность не достигнута. Интеграл не имеет решения."); p = 0; }
                }
            }
            else { rez = 0; p = 0; throw new ArgumentException("Пределы интегрирования равны, результат вычисления будет равен 0 в любом случае."); } 
            // учитывание условия равенства нижнего и верхнего пределов
            Tuple<double, double, double, bool> results = new Tuple<double, double, double, bool>(rez, p, uac, flag);
            return results;
        }
        
        public static double Point(double x, double num) // вычисление значения выбранной пользователем функции в точке х 
        {
            if (num == 1)  return 2 * Math.Pow(x, 2) + 3 * x - 1; // 1
            if (num == 2) return 1 / (1 + x);  // 2 
            else return Math.Sqrt(1 + Math.Pow(x, 2));
        }
    }
}