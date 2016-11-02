using System;
using System.IO;
using System.Collections.Generic;

namespace FileTracker
{
    public class FileStats
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
        public decimal Average { get; set; }
        public decimal StDev { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public List<decimal> NumArray { get; set; }
        public int NumCount { get; set; }
        public string[] StringArray { get; set; }
        public bool ErrCheck { get; set; }

        public FileStats(string name, string[] numStrings, int numCount)
        {
            this.Name = name;
            this.NumCount = numCount;
            this.Total = 0;
            this.Average = 0;
            this.StDev = 0;
            this.Min = Decimal.MaxValue;
            this.Max = Decimal.MinValue;
            this.NumArray = new List<decimal>();;
            this.StringArray = numStrings;
            this.ErrCheck = false;
        }

        public void ParseStrings ()
        {
            decimal num;
            foreach (var stringNum in StringArray)
            {   
                if (Decimal.TryParse(stringNum, out num))
                {
                    Min = (num < Min) ? num : Min;
                    Max = (num > Max) ? num : Max;
                    Total += num;
                    NumArray.Add(num);
                }
                else
                {
                    ErrCheck = true;
                }        
            }
            Average = Total / NumCount;
        }

        public void DeviationCalculation()
        {
            decimal sumOfSquares = 0;
            foreach (var number in NumArray)
            {
                sumOfSquares += (decimal)Math.Pow((double)(number - Average), 2);
            }
            StDev = (decimal)Math.Sqrt(((double)(sumOfSquares)/NumCount));
        }

        public bool ErrorTracker ()
        {
            return ErrCheck;
        }
        
        public void PrintMe()
        {
            Console.WriteLine("File Name: {0}", Name);
            Console.WriteLine("Sum: {0}", Math.Truncate(Total * 1000) / 1000);
            Console.WriteLine("Average: {0}", Math.Truncate(Average * 1000) / 1000); 
            Console.WriteLine("StDev: {0}", Math.Truncate(StDev * 1000) / 1000);
            Console.WriteLine("Max: {0}", Math.Truncate(Max * 1000) / 1000);
            Console.WriteLine("Min: {0}\n", Math.Truncate(Min * 1000) / 1000);
        } 
    }
}        


        
