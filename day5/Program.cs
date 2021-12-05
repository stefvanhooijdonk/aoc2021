using System;
using System.Collections.Generic;


namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2021 - day 5.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = new List<string>(System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt")));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.Count);

            var grid = new Grid(1000, 1000);
            var gridLines = new List<Line>();

            foreach (var inputLine in lines)
            {
                gridLines.Add(GetLine(inputLine));
            }
            Console.WriteLine("Number of Lines  {0}", gridLines.Count);

            var horizontalLines = gridLines.FindAll(line => line.Start.X == line.End.X);
            foreach (var line in horizontalLines)
            {
                var pointsDrawn = grid.DrawLineOnGrid(line);
                // Console.WriteLine("Line drawn {0},{1} -> {2},{3} and hit {4} points",
                //     line.Start.X,
                //     line.Start.Y,
                //     line.End.X,
                //     line.End.Y,
                //     pointsDrawn);
            }
            Console.WriteLine("H Lines applied to Grid {0}", horizontalLines.Count);

            var verticalLines = gridLines.FindAll(line => line.Start.Y == line.End.Y);
            foreach (var line in verticalLines)
            {
                var pointsDrawn = grid.DrawLineOnGrid(line);
                // Console.WriteLine("Line drawn {0},{1} -> {2},{3} and hit {4} points",
                //     line.Start.X,
                //     line.Start.Y,
                //     line.End.X,
                //     line.End.Y,
                //     pointsDrawn);
            }
            Console.WriteLine("V Lines applied to Grid {0}", verticalLines.Count);


            var pointsWithMoreThanOneIntersection = grid.Points.FindAll(p => p.Intersections >= 2);

            Console.WriteLine("Step 1: Number of points with more than one intersection {0}",             pointsWithMoreThanOneIntersection.Count);

            grid = new Grid(1000, 1000);

            foreach (var line in gridLines)
            {
                var pointsDrawn = grid.DrawLineOnGrid(line);
                // Console.WriteLine("Line drawn {0},{1} -> {2},{3} and hit {4} points",
                //     line.Start.X,
                //     line.Start.Y,
                //     line.End.X,
                //     line.End.Y,
                //     pointsDrawn);
            }
            Console.WriteLine("All Lines applied to Grid {0}", gridLines.Count);

            pointsWithMoreThanOneIntersection = grid.Points.FindAll(p => p.Intersections >= 2);

            Console.WriteLine("Step 2: Number of points with more than one intersection {0}",             pointsWithMoreThanOneIntersection.Count);

        }

        internal static Line GetLine(string lineFromInput)
        {
            var result = new Line();

            if (string.IsNullOrEmpty(lineFromInput))
            {
                return null;
            }

            var lineWithSemiColon = lineFromInput.Replace(" -> ", ";");
            var twoParts = lineWithSemiColon.Split(';');
            result.Start = GetPoint(twoParts[0]);
            result.End = GetPoint(twoParts[1]);

            return result;
        }

        internal static Point GetPoint(string pointData)
        {
            var twoParts = pointData.Split(',');
            return new Point(
                int.Parse(twoParts[0]),
                int.Parse(twoParts[1]));
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Intersections = 0;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int Intersections { get; set; }

        public bool IntersectWithLine(Line line)
        {
            var result = 
            // on a horizontal line
            (line.Start.X == line.End.X) && (X == line.Start.X) && InRange(line.Start.Y, line.End.Y, this.Y) ||
            // on a vertical line
            (line.Start.Y == line.End.Y) && (Y == line.Start.Y) && InRange(line.Start.X, line.End.X, this.X) ||
            // on a diagnal line
            (
                InRange(line.Start.Y, line.End.Y, this.Y) &&
                InRange(line.Start.X, line.End.X, this.X) &&
                (Math.Abs(this.X - line.Start.X) == Math.Abs(this.Y - line.Start.Y))
            );

            if (result) this.Intersections++;
            return result;
        }

        private bool InRange(int x, int y, int checkNumber)
        {

            if (x > y)
            {
                return y <= checkNumber && checkNumber <= x;
            }
            else
            {
                return x <= checkNumber && checkNumber <= y;
            }
        }
    }

    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class Grid
    {
        public Grid(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    this.Points.Add(new Point(x, y));
                }
            }
        }

        public List<Point> Points = new List<Point>();

        public int DrawLineOnGrid(Line line)
        {
            var pointsDraw = 0;
            foreach (var p in Points)
            {
                if (p.IntersectWithLine(line))
                {
                    pointsDraw++;
                };
            }

            return pointsDraw;
        }
    }
}
