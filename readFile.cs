using System;
using System.IO;
using FileTracker;

namespace FileParser
{
    class Parser
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var directoryList = directory.GetFiles("*.data");

            foreach (var dataFile in directoryList)
            {
                string stringFile = dataFile.ToString();
                string justFileName = Path.GetFileName(stringFile);
                string[] fileNums = StringSplitter(stringFile);
                FileStats Stats = new FileStats(justFileName, fileNums, fileNums.Length);                
                
                Stats.ParseStrings();

                if (!Stats.ErrCheck)
                {
                    Stats.DeviationCalculation();
                    Stats.PrintMe();
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

        public static string[] StringSplitter(string file)
        {
            string fileContents = ReadFile(file);
            string[] fileNums = fileContents.Split(new char[] {' '});
            return fileNums;
        }
    }
}