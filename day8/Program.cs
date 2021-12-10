using System;
using System.Collections.Generic;

namespace day8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 8.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt")));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.Count);

            var Samples = new List<Sample>();

            foreach(var line in lines){
                Samples.Add(new Sample(line));
            }

            var totalEasyDigitsCounted = 0;
            Samples.ForEach(s=> totalEasyDigitsCounted+= s.CountEasyDigits());

            Console.WriteLine("Step 1: easy digits found {0}.\r\n",totalEasyDigitsCounted);

        }
    }

    public class Sample{
        public Sample(string input){
            var parts = input.Split('|');
            InputSingals = new List<string>(parts[0].Trim().Split(' '));
            OutputDigits = new List<string>(parts[1].Trim().Split(' '));
        }

        public List<string> InputSingals {get;set;}
        public List<string> OutputDigits {get;set;}

        public int CountEasyDigits(){
            var result = OutputDigits.FindAll(d => d.Length == 2 ).Count; // digit 1
            result += OutputDigits.FindAll(d => d.Length == 4 ).Count;// digit 4
            result += OutputDigits.FindAll(d => d.Length == 3 ).Count;// digit 7
            result += OutputDigits.FindAll(d => d.Length == 7 ).Count;// digit 8
            return result;
        }
    }
    public class DigitSegments
    {
        public DigitSegments(int digit, string segments){
            Digit = digit;
            //Segments = segments.ToCharArray();
        }

        public int Digit{get;set;}
        public List<char> Segments {get;set;} = new List<char>();
    }
    public class SolvedSample{

        public SolvedSample(Sample sample){
            Digits.Add(0,"");
            Digits.Add(1,"");
            Digits.Add(2,"");
            Digits.Add(3,"");
            Digits.Add(4,"");
            Digits.Add(5,"");
            Digits.Add(6,"");
            Digits.Add(7,"");
            Digits.Add(8,"");
            Digits.Add(9,"");
            sampleToSolve = sample;            
        }

        private Sample sampleToSolve;

        public List<DigitSegments> Digits = new List<DigitSegments>();

        public bool Solve (){

            var edgesToCheck = "abcdefg";

            while(Mapping.Count<10){
                foreach(var edge in edgesToCheck)
                {
                    if(edge == 'a') // 0,2,3,5,6,7,8,9
                    {
                        var options = GetSegments(OutputDigits.FindAll(d => d.Length == 3 )); //7
                        var sureNotOptions = GetSegments(OutputDigits.FindAll(d => d.Length == 1 )); // 1

                        options.RemoveAll(p=> sureNotOptions.Contains(p));
                        if(options.Length == 1){
                            Mapping.Add('a', options);
                        }
                    }

                    if (edge == 'g') 
                    {
                        var options = GetSegments(OutputDigits.FindAll(d => d.Length == 4 || d.Length == 3)); //4&7
                        var nineOptions = OutputDigits.FindAll(d => d.Length == 6 ); // possible 9

                        var nine = nineOptions.FindAll( d => d.InputSingals.Findall() )
                        if(options.Length == 1){
                            Mapping.Add('a', options);
                        }
                    }
                }
            }

            return false;
        }

        pri

        private List<char> GetSegments(List<DigitSegments> digits){

            var chars = new List<char>();

            foreach(var d in digits){
                foreach(var s in d.Segments){
                    if(!chars.Contains(s)){
                        chars.Add(s);
                    }
                }
            }
            return chars;
        }

        public int Output(){

            return 0;
        }

        private Dictionary<char,List<char>> Mapping = new Dictionary<char, List<char>>();
    }
}
