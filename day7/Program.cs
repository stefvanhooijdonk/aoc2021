using System;
using System.Collections.Generic;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 7.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt")));

            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.Count);


            List<Crab> crabs = new List<Crab>();
            var positions = lines[0].Split(',');
            foreach (var position in positions)
            {
                crabs.Add(new Crab(position));
            }

            var minimumFuel = Int64.MaxValue;

            for(var positionToCheck = 0;positionToCheck<=2000;positionToCheck++)
            {
                var fuelNeededForPosition = GetTotalFuel(crabs,positionToCheck);
                if(fuelNeededForPosition< minimumFuel)
                {
                    minimumFuel = fuelNeededForPosition;
                }
            }

            Console.WriteLine("Minimal fuel needed optimum: {0}.\r\n", minimumFuel);

            minimumFuel = Int64.MaxValue;

            for(var positionToCheck = 0;positionToCheck<=2000;positionToCheck++)
            {
                var fuelNeededForPosition = GetTotalFuel2(crabs,positionToCheck);
                if(fuelNeededForPosition< minimumFuel)
                {
                    minimumFuel = fuelNeededForPosition;
                }
            }


            Console.WriteLine("Minimal fuel2 needed optimum: {0}.\r\n", minimumFuel);

        }

        private static Int64 GetTotalFuel(List<Crab> crabs, Int64 position){

            var result = 0L;
            crabs.ForEach(c=> result+= c.FuelNeeded(position));
            return result;
        }

        private static Int64 GetTotalFuel2(List<Crab> crabs, Int64 position){

            var result = 0L;
            crabs.ForEach(c=> result+= c.Fuel2Needed(position));
            return result;
        }   
    }

    public class Crab
    {

        public Crab(string horizontalposition)
        {
            this.HorizontalPosition = Int64.Parse(horizontalposition);         
        }

        public Int64 HorizontalPosition
        {
            get; set;
        }
        public Int64 FuelNeeded(Int64 horizontalposition)
        {
            var distance = Math.Abs(this.HorizontalPosition - horizontalposition);
            return distance;
        }

        public Int64 Fuel2Needed(Int64 horizontalposition)
        {
            var distance = Math.Abs(this.HorizontalPosition - horizontalposition);

            var fuel = 0L;
            fuel =  (distance + 1) * (distance / 2);
            if(is_odd(distance)){
                 fuel = ((distance + 1) * ((distance - 1)/ 2)) + (((distance - 1)/ 2) + 1);
            }
            else{
                 fuel =  (distance + 1) * (distance / 2);
            }
            return fuel;
        }

        private bool is_odd(Int64 n) {
            return n % 2 == 1; 
        }
    }
}
