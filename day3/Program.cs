using System;
using System.Collections.Generic;

namespace day3
{
    class Program
    {
        public class Submarine
        {
            public Submarine(int gamma, int epsilon){
                GammaRate = gamma;
                EpsilonRate = epsilon;
            }

            int GammaRate;
            int EpsilonRate;

            public int PowerConsumption() {
                return GammaRate * EpsilonRate;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 3.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var diagnostics = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt")));
            Console.WriteLine("Read input.txt with: {0} instructions.\r\n",diagnostics.Count);

            var bitLength = 12;

            var gamma = 0;
            var epsilon = 0;

            for(int column=0;column<bitLength;column++)
            {
                var bitValue = Convert.ToInt32(Math.Pow(2,(bitLength-column-1)));

                if(MostCommonBitInPosition( diagnostics, column) == 1)
                    gamma += bitValue;
                else
                    epsilon += bitValue;
            }

            var sub = new Submarine(gamma, epsilon);

            Console.WriteLine("Found \r\nGamma:           {0}\r\nEpsilon:         {1}\r\nStep 1 answer: Powerconsumption {2} .\r\n",gamma,epsilon,sub.PowerConsumption());

            var oxygen = FindSingleValueForMostCommonBit(diagnostics);
            var cotwo = FindSingleValueForLeastCommonBit(diagnostics);
            
            Console.WriteLine("oxygen generator rating {0}",oxygen);
            Console.WriteLine("CO2 scrubber rating {0}",cotwo);
            Console.WriteLine("Step 2 answer {0}",oxygen*cotwo);

        }

        internal static int MostCommonBitInPosition(List<string> items,int position){
            var bitsCounted = 0;
            var zerosCounted = 0;
            foreach(var diagnosticLine in items){
                if(diagnosticLine[position] == '1' )
                    bitsCounted++;
                else
                    zerosCounted++;
            }
            return bitsCounted>=zerosCounted ? 1 : 0;
        }

        internal static int FindSingleValueForMostCommonBit(List<string> items)
        {
            var depth = 0;
            var mcb = MostCommonBitInPosition(items,depth);
            var subitems = GetSubCollection(items,depth,mcb);
            while(subitems.Count > 1)
            {   depth++;
                mcb = MostCommonBitInPosition(subitems,depth);
                subitems = GetSubCollection(subitems,depth,mcb);
            }
            return ConvertBitStringToInt(subitems[0]);
        }

        internal static int FindSingleValueForLeastCommonBit(List<string> items)
        {
            var depth = 0;
            var mcb = MostCommonBitInPosition(items,depth);
            var subitems = GetSubCollection(items,depth,1-mcb);
            while(subitems.Count > 1)
            {   depth++;
                mcb = MostCommonBitInPosition(subitems,depth);
                subitems = GetSubCollection(subitems,depth,1-mcb);
            }
            return ConvertBitStringToInt(subitems[0]);
        }

        internal static int ConvertBitStringToInt(string bitValue,int bitLength = 12){            
            var result = 0;
            for(int column=0;column<bitLength;column++)
            {
                if(bitValue[column]=='1'){
                    result += Convert.ToInt32(Math.Pow(2,(bitLength-column-1)));
                }
            }

            return result;
        }

        internal static List<string> GetSubCollection(List<string> items, int position, int bit)
        {   
            var bitToLookupFor = '1';
            if(bit == 0) bitToLookupFor = '0';
            return items.FindAll(item => item[position] == bitToLookupFor);
        }
    }
}
