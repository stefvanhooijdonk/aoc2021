using System;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 1.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var depths = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} measurements.\r\n",depths.LongLength);

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("How many measurements are larger than the previous measurement?");

            // can a depth ever be Int64.MaxValue?
            var previousDepthInteger = Int64.MaxValue;
            var depthsIncreased = 0;
            foreach (var depth in depths)
            {
                var depthInteger = Convert.ToInt64(depth);
                if(previousDepthInteger < depthInteger){
                    depthsIncreased++;
                }
                previousDepthInteger = depthInteger;
            }

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Measurements are larger than the previous measurement = {0}.\r\n", depthsIncreased);
        }
    }
}
