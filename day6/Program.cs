using System;
using System.Collections.Generic;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 6.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt")));

            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.Count);

            var fc80 = new LargeFishCollection(lines[0]);
            fc80.ProcessMultipleDays(80);
            Console.WriteLine("After 80 days, total fish {0}.\r\n", fc80.TotalFish());

            var fc256 = new LargeFishCollection(lines[0]);
            var measure = new System.Diagnostics.Stopwatch();
            measure.Start();
            fc256.ProcessMultipleDays(256);
            measure.Stop();
            var microseconds = measure.ElapsedTicks /(System.Diagnostics.Stopwatch.Frequency / (1000L*1000L));
            Console.WriteLine("After 256 days, total fish {0} in {1}ms.\r\n", 
                fc256.TotalFish(),
                microseconds);
        }
    }
    
    public class LargeFishCollection
    {
        public LargeFishCollection(string input)
        {
            var fishes = input.Split(',');
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);
            Fishes.Add(0);

            foreach(var fish in fishes)
            {
                var fishInt = int.Parse(fish);
                Fishes[fishInt]++;
            }
        }

        public List<Int64> Fishes = new List<Int64>();

        public void ProcessADay()
        {
            var newFishSpawned = 0L;
            for(int f = 0 ;f <= 8; f ++){
                if(f==0){
                    newFishSpawned = Fishes[f];
                    Fishes[f] = Fishes[f+1];
                }
                else if (f==6) 
                {
                    Fishes[f] = Fishes[f+1] + newFishSpawned;
                }
                else if (f==8) 
                {
                    Fishes[f] = newFishSpawned;
                }
                else{
                    Fishes[f] = Fishes[f+1];
                }
            }
        }

        public void ProcessMultipleDays(int days){
            for(int day = 0; day<days;day++){
                ProcessADay();
            }
        }

        public Int64 TotalFish()
        {
            var result = 0L;
            Fishes.ForEach(fg=> result += fg);
            return result;
        }
    }
}
