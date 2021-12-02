using System;

namespace day2
{
    class Program
    {
        const string forward = "forward";
        const string up = "up";
        const string down = "down";

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 2.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var instructions = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} instructions.\r\n",instructions.LongLength);

            var totalForwards = 0;
            var depthReached = 0;
            var instructionsProcessed = 0;
            var currentAim = 0;
            var depthReachedWithAim = 0;

            foreach(var instruction in instructions){

                if (!string.IsNullOrEmpty(instruction)){
                    
                    var instructionDirectionValue = instruction.Split(' ').GetValue(1);

                    if(instruction.StartsWith(forward)){
                        var forwardInt = Convert.ToInt32(instructionDirectionValue);
                        totalForwards += forwardInt;
                        depthReachedWithAim += (currentAim * forwardInt);

                        instructionsProcessed++;
                    }
                    if(instruction.StartsWith(up)){
                        depthReached -= Convert.ToInt32(instructionDirectionValue);
                        currentAim = depthReached;
                        instructionsProcessed++;
                    }
                    if(instruction.StartsWith(down)){
                        depthReached += Convert.ToInt32(instructionDirectionValue);
                        currentAim = depthReached;
                        instructionsProcessed++;
                    }
                }
            }

            Console.WriteLine("Processed {0} instructions.",instructionsProcessed);

            Console.WriteLine("Step 1.\r\n");

            Console.WriteLine("Forward movements total {0}.",totalForwards);
            Console.WriteLine("Depth reached {0}.\r\n",depthReached);
            Console.WriteLine("What do you get if you multiply your final horizontal position by your final depth {0}.\r\n",depthReached*totalForwards);

            Console.WriteLine("Step 2.\r\n");
            Console.WriteLine("Forward movements total {0}.",totalForwards);
            Console.WriteLine("Depth reached {0}.\r\n",depthReachedWithAim);
            Console.WriteLine("What do you get if you multiply your final horizontal position by your final depth {0}.\r\n",depthReachedWithAim*totalForwards);

        }
    }
}
