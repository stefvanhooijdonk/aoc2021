using System;
using System.Collections.Generic;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 4.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt")));
            Console.WriteLine("Read input.txt with: {0} instructions.\r\n",lines.Count);

            var drawing = GetListOfNumbersToDraw(lines);

            var cards = new List<BingoCard>();
            var cardLines = new List<string>();

            foreach(var inputline in lines)
            {
                if(string.IsNullOrEmpty(inputline)){
                    cardLines = new List<string>();
                }
                else{
                    cardLines.Add(inputline);

                    if(cardLines.Count == 5){
                        var card = new BingoCard();
                        cardLines.ForEach(l => card.AddRow(l));
                        cards.Add(card);

                        cardLines = new List<string>();
                    }
                }
            }

            List<BingoCard> cardsWithoutBingo = cards;

            foreach(var draw in drawing){
                 cards.ForEach(c=>c.AddMark(draw));
                 var bingo = cards.Find(c=>c.Bingo());

                 if(bingo!=null) {
                     Console.WriteLine("Found a bingo with card having unmarked sum {0}",bingo.SumRows(false));
                     Console.WriteLine("Answer step 1 : {0}",draw * bingo.SumRows(false));
                     break;
                 }
            }
            
            foreach(var draw in drawing){
                cardsWithoutBingo.ForEach(c=>c.AddMark(draw));
                
                if(cardsWithoutBingo.Count<=1 && cardsWithoutBingo[0].Bingo()) {
                    Console.WriteLine("Found the last bingo with card having unmarked sum {0}",cardsWithoutBingo[0].SumRows(false));
                    Console.WriteLine("Answer step 2: {0}",draw * cardsWithoutBingo[0].SumRows(false));
                    break;
                }
                
                cardsWithoutBingo.RemoveAll(c=>c.Bingo());
            }
        }

        private static List<int> GetListOfNumbersToDraw(List<string> input){
            var drawing = new List<int>();
            var inputDrawingNumbers = input[0];
            var inputAsArray = inputDrawingNumbers.Split(',');

            for(int c = 0; c < inputAsArray.GetLength(0); c++){
                drawing.Add( Convert.ToInt32(inputAsArray[c]));
            }

            return drawing;
        }
    }

    public class BingoCardNumber
    {
        public BingoCardNumber(string input)
        {
            Number = Convert.ToInt32(input.Trim());
        }

        public bool Marked = false;
        public int Number = -1;
    }

    public class Row
    {
        public Row(string lineOfNumbers)
        {
            var nrs = lineOfNumbers.Trim().Split(' ');
            foreach(var nr in nrs){
                if(!string.IsNullOrEmpty(nr)){
                    Numbers.Add(new BingoCardNumber(nr));
                }
            }
        }

        public List<BingoCardNumber> Numbers = new List<BingoCardNumber>();

        public bool Marked 
        {
            get 
            {
                return Numbers.FindAll(n=>n.Marked).Count == 5;
            }
        }

        public int SumRow(bool marked) 
        {
            var sum = 0;
            Numbers.FindAll(nr => nr.Marked==marked ).ForEach(n => sum += n.Number);
            return sum;
        
        }

        public void Mark(int nr)
        {
            Numbers.FindAll(n=>n.Number == nr).ForEach(marking=> marking.Marked=true);
        }
    }

    public class BingoCard
    {
        public BingoCard(){

        }

        public void AddRow(string line){
            this.Rows.Add(new Row(line));
        }

        public List<Row> Rows = new List<Row>();

        public void AddMark(int number)
        {
            foreach(var r in Rows)
            {
                r.Mark(number);
            }
        }

        public bool Bingo()
        {

            var bingo = Rows.FindAll(r => r.Marked).Count >= 1;

            if (bingo) return bingo;

            for(var column = 0; column < 5; column++)
            {
                bingo = Rows.FindAll(r => r.Numbers[column].Marked).Count == 5;
                if (bingo) break;
            }

            return bingo;
        }

          public int SumRows(bool marked) {
                var sum = 0;
                Rows.ForEach(n=> sum += n.SumRow(marked));
                return sum;
        }
    }
}
