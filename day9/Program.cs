using System;
using System.Linq;
using System.Collections.Generic;

namespace day9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 9.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt")));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.Count);

            Grid grid = new Grid();
            
            for(var row =0; row<lines.Count;row++){
                grid.AddRow(row, lines[row]);
            }

            var lowPoints = grid.GetLowPoints();

            var step1 = 0;
            lowPoints.ForEach(lp => step1 += (1 + lp.CellValue));
            Console.WriteLine("Found lowpoints: {0} risk {1}.\r\n",lowPoints.Count, step1);

            
            List<int> basinSizes = new List<int>();

            foreach(var lowpoint in lowPoints){
                var basin = new List<GridCell>();
                grid.FindBasin(lowpoint.X, lowpoint.Y, ref basin);
                basinSizes.Add(basin.Count);
            }
            basinSizes.Sort();
            var step2 = basinSizes[basinSizes.Count-1] * basinSizes[basinSizes.Count-2] * basinSizes[basinSizes.Count-3];
            Console.WriteLine("Found basins {0}.\r\n", step2);
        }
    }

    public class Grid {
        public Grid(){

        }

        public int MaxY{get;set;}
        public int MaxX{get;set;}

        public List<GridCell> Cells {get;set;} = new List<GridCell>();

        public void AddRow(int row, string values){

            for(int y=0; y<values.Length;y++){
                int value = int.Parse(values[y].ToString());
                Cells.Add(new GridCell(row,y,value));
                if(y>MaxY) MaxY = y;
            }
            MaxX++;
        }

        public List<GridCell> GetLowPoints(){
            List<GridCell> lowpoints = new List<GridCell>();

            foreach(var cell in Cells){
                var neighbours = GetNeighbours(cell);

                if(neighbours.FindAll(n=> n.CellValue > cell.CellValue).Count == neighbours.Count){
                    lowpoints.Add(cell);
                }
            }

            return lowpoints;
        }

        private List<GridCell> GetNeighbours(GridCell cell){
            List<GridCell> neighbours = new List<GridCell>();

            if(cell.X > 0){
                neighbours.AddRange(Cells.FindAll(c=> (c.X == cell.X-1) && (c.Y == cell.Y)));
            }
            if(cell.X < MaxX){
                neighbours.AddRange(Cells.FindAll(c=> (c.X == cell.X+1) && (c.Y == cell.Y)));
            }
            if(cell.Y > 0){
                neighbours.AddRange(Cells.FindAll(c=> (c.Y == cell.Y-1) && (c.X == cell.X)));
            }
            if(cell.Y < MaxY){
                neighbours.AddRange(Cells.FindAll(c=> (c.Y == cell.Y+1) && (c.X == cell.X)));
            }

            return neighbours;
        }

        public void FindBasin(int x, int y, ref List<GridCell> basin){
            var cell = Cells.Find(c=> c.X==x && c.Y==y);
            if(cell?.CellValue<9 && !basin.Contains(cell)){
                basin.Add(cell);
                 
                if( cell.X > 0 ){
                    FindBasin(x-1, y, ref basin);
                }
                if( cell.X < MaxX ){
                    FindBasin(x+1, y, ref basin);
                }
                if( cell.Y > 0 ){
                     FindBasin(x, y-1, ref basin);
                }
                if( cell.Y < MaxY ){
                    FindBasin(x, y+1, ref basin);
                }
            }
        }        
    }

    public class GridCell {

        public GridCell(int locationX, int locationY, int value){
            X=locationX;
            Y=locationY;
            CellValue= value;
        }

        public int X {get;set;}
        public int Y {get;set;}
        public int CellValue {get;set;}
    }
}
