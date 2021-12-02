using System;

namespace day1
{
    class Program
    {
        class SlidingWindow{
            public Int64 entry1 = 0;
            public Int64 entry2 = 0;
            public Int64 entry3 = 0;

            public void AddEntryToWindow(Int64 entry){
                entry1 = entry2;
                entry2 = entry3;
                entry3 = entry;
            }

            public Int64 GetSum(){
                if(entry3>0 && entry2>0 && entry1>0)
                    return entry1+entry2+entry3;
                else
                    return Int64.MinValue;
            }

        }
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
            var slidingWindow = new SlidingWindow();
            var previousSlidingWindowSum = Convert.ToInt64(0);
            var slidingWindowsIncreased = 0;

            for(int i=0;i<depths.LongLength;i++)
            {
                var depth = depths[i];
                var depthInteger = Convert.ToInt64(depth);
                if(previousDepthInteger < depthInteger){
                    depthsIncreased++;
                }
                previousDepthInteger = depthInteger;
                
                slidingWindow.AddEntryToWindow(depthInteger);
                var currentWindowSum = slidingWindow.GetSum();

                if(currentWindowSum > Int64.MinValue && i <= depths.LongLength-2){
                    if(currentWindowSum>previousSlidingWindowSum){
                        slidingWindowsIncreased++;
                    }
                    previousSlidingWindowSum = currentWindowSum;
                }
            }

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Step 1");
            System.Console.WriteLine("Measurements are larger than the previous measurement = {0}.\r\n", 
            depthsIncreased);
            System.Console.WriteLine("Step 2");
            System.Console.WriteLine("Measurement windows are larger than the previous measurement = {0}.\r\n", slidingWindowsIncreased);

        }
    }
}
