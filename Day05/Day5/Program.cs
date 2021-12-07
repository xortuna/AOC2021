using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        class Line {

            public Line(int x1, int y1, int x2, int y2)
            {
                xStart = x1;
                xEnd = x2;
                yStart = y1;
                yEnd = y2;
                xDist = Math.Max(xStart, xEnd) - Math.Min(xStart, xEnd);
                yDist = Math.Max(yStart, yEnd) - Math.Min(yStart, yEnd);
            }
            public int xStart, yStart, xEnd, yEnd;
            public int xDist, yDist;

            internal void Paint(ref int[,] grid)
            {
                int steps = Math.Max(xDist, yDist);
                for (int s = 0; s <= steps; ++s )
                {
                    int xPos = xStart + (((xEnd-xStart) * s) / steps);
                    int yPos =  yStart + (((yEnd-yStart)* s) / steps);
                    grid[xPos, yPos]++;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Part 1
            List<Line> lines = new List<Line>();
            using (StreamReader sr = new StreamReader("puzzleinput.txt"))
            {
                Regex coordParser = new Regex(@"(\d+),(\d+)\s->\s(\d+),(\d+)");
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    var m = coordParser.Match(line);
                    if(m.Success)
                    {
                        lines.Add(new Line(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value)));
                    }
                }

            }

            int[,] grid = new int[lines.Max(r => Math.Max(r.xStart, r.xEnd)) + 1, lines.Max(r => Math.Max(r.yStart, r.yEnd)) + 1];
            foreach(var line in lines.Where(t=> (t.xStart == t.xEnd|| t.yStart == t.yEnd)))
            {
                line.Paint(ref grid);

            }
            //PrintGrid(grid);
            Console.WriteLine($"Part 1: {CountGeezers(grid)}");

            grid = new int[lines.Max(r => Math.Max(r.xStart, r.xEnd)) + 1, lines.Max(r => Math.Max(r.yStart, r.yEnd)) + 1];
            foreach (var line in lines)
            {
                line.Paint(ref grid);
            }
            //PrintGrid(grid);
            Console.WriteLine($"Part 2: {CountGeezers(grid)}");
            Console.ReadLine();
        }

        private static int CountGeezers(int[,] grid)
        {
            var c = 0;
            for (int y = 0; y < grid.GetLength(1); ++y)
            {
                for (int x = 0; x < grid.GetLength(0); ++x)
                {
                    if (grid[x, y] > 1)
                        c++;
                }
            }
            return c;
        }

        private static void PrintGrid(int[,] grid)
        {
            Console.WriteLine();
            for (int y = 0; y < grid.GetLength(1); ++y)
            {
                
                for (int x = 0; x < grid.GetLength(0); ++x)
                {
                    Console.Write(grid[x, y] + ",");
                }
                Console.WriteLine();
            }
        }
    }
}
