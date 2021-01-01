using Lab3.Services.Parsers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class InterpolaterInput
    {
        //firstPointX, step, pointsCount, offset, pointToChange, funcNumber
        public double[] XData { get; set; }
        public int FuncType { get; set; }
        public double Offset { get; set; }
        public int PointToChangeIndex { get; set; }
        public double Step { get; set; }

        public static InterpolaterInput getFromRequest(HttpContext context)
        {
            string strFirstPointX = context.Request.Query["firstPointX"];
            string stepString = context.Request.Query["step"];
            string sPointsCount = context.Request.Query["pointsCount"];
            string sPointToChane = context.Request.Query["pointToChange"];
            string sType = context.Request.Query["funcNumber"];
            string stringOffset = context.Request.Query["offset"];

            // for linux use replacing from , to .
            stringOffset = stringOffset.Replace(',', '.');
            double offset = double.Parse(stringOffset);
            int type = int.Parse(sType);
            double firstPointX = double.Parse(strFirstPointX);
            double step = double.Parse(stepString);
            int pointsCount = int.Parse(sPointsCount);
            int pointToChange = int.Parse(sPointToChane);
            double[] xData = getXData(firstPointX, step, pointsCount);
            
            Array.Sort(xData);


            foreach (double d in xData)
            {
                Console.WriteLine(d);
            }
            
            return new InterpolaterInput
            {
                FuncType = type,
                XData = xData,
                Offset = offset,
                PointToChangeIndex = pointToChange,
                Step = step
            };
        }
        
        
        public static double[] getXData(double firstPointX, double step, int pointsCount)
        {
            
            double[] xData = new double[pointsCount];
            for (int i = pointsCount - 1; i >= 0; i--)
            {
                xData[i] = firstPointX;
                firstPointX += step;
                
            }

            return xData;
        }
    }
}