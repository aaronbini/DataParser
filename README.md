# DataParser
## A C# Console Application for Parsing Data

###Instructions

Git clone this repository, which contains a few sample data files to get started.

Run this program from the command line. I recommed a C# compiler called [Mono](http://www.mono-project.com/).

In order to run with Mono, enter: mcs -out:runCruncher.exe *.cs

Then enter: mono runCruncher.exe

It will process any files in the same directory that end in .data, and will print statistical information for each file.

The format for the data files must be a string representing space-separated numbers: 123.23 45 345.4 44545.3, etc.

Will print: sum of all numbers, min, max, average, standard deviation, or an error message if the file was not formatted correctly.