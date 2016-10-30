//use the StreamReader class to read in file contents in a particular encoding
//use Console class to write results to the console
using System;
using System.IO;

//TODO: clean up output (remove some decimals)
//TODO: modularize code (pull out fileStats class into separate module)
//TODO: shore up understanding of terms: namespace, public/private
//var vs. explicit type declaration, remove some comments

namespace FileParser
{
    class Parser
    {
        static void Main(string[] args)
        {
            //the above string[] args passed to Main are the command line arguments
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var directoryList = directory.GetFiles("*.data");
            //now directoryList is array of path & filenames that end in .data
            foreach (var dataFile in directoryList)
            {
                string stringFile = dataFile.ToString();
                string justFileName = Path.GetFileName(stringFile);
                var fileContents = ReadFile(stringFile);
                string[] fileNums = fileContents.Split(new char[] {' '});                
                decimal sumAccumulator = 0;
                int count = fileNums.Length;
                decimal standardDeviation = 0;
                decimal minimum = Decimal.MaxValue;
                decimal maximum = Decimal.MinValue;
                decimal averageValue = 0;
                decimal num;
                bool errCheck = false;
                foreach (var stringNum in fileNums)
                {   
                    // TryParse returns true if the conversion succeeded
                    // and stores the result in num
                    if (!Decimal.TryParse(stringNum, out num))
                    {
                        errCheck = true;
                    }
                    else
                    {
                        minimum = (num < minimum) ? num : minimum;
                        maximum = (num > maximum) ? num : maximum;
                        sumAccumulator += num;
                    }        
                }
                averageValue = sumAccumulator / count;
                decimal sumOfSquares = 0;
                foreach (var stringNum in fileNums)
                {
                    decimal stDevNum;
                    if (Decimal.TryParse(stringNum, out stDevNum))
                    {
                        sumOfSquares += (decimal)Math.Pow((double)(stDevNum - averageValue), 2);
                    }
                }
                standardDeviation = (decimal)Math.Sqrt(((double)(sumOfSquares)/count));
                if (!errCheck)
                {
                    FileStats fs = new FileStats(justFileName, sumAccumulator, averageValue, standardDeviation, minimum, maximum);
                    fs.printMe();
                }
                else
                {
                    Console.WriteLine("Filename: {0}", justFileName);
                    Console.WriteLine("Bad Value In File. NaN.\n");
                }
            }   
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public class FileStats
        {
            public string Name { get; set; }
            public decimal Total { get; set; }
            public decimal Average { get; set; }
            public decimal StDev { get; set; }
            public decimal Min { get; set; }
            public decimal Max { get; set; }

            public FileStats(string name, decimal total, decimal average, decimal stDev, decimal min, decimal max)
            {
                Name = name;
                Total = total;
                Average = average;
                StDev = stDev;
                Min = min;
                Max = max;
            }
            
            public void printMe()
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
}