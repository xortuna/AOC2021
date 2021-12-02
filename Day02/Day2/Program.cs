using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Part 1
            int HorizontalPosition = 0;
            int Depth = 0;
            string line;
            Regex lineSplit = new Regex( @"(\w+)\s(\d)");
            using (StreamReader sr = new StreamReader("puzzleinput.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var s = lineSplit.Match(line);
                    if(s.Success)
                    {
                        int distance = int.Parse(s.Groups[2].Value);
                        switch (s.Groups[1].Value) //direction
                        {
                            case "forward":
                                HorizontalPosition += distance;
                                break;
                            case "down":
                                Depth += distance;
                                break;
                            case "up":
                                Depth -= distance;
                                break;

                        }

                    }
                }

                Console.WriteLine($"F:{HorizontalPosition} D:{Depth} Part 1:{HorizontalPosition * Depth}");
            }
            int Aim = 0;
            Depth = HorizontalPosition = 0;
            using (StreamReader sr = new StreamReader("puzzleinput.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var s = lineSplit.Match(line);
                    if (s.Success)
                    {
                        int distance = int.Parse(s.Groups[2].Value);
                        switch (s.Groups[1].Value) //direction
                        {
                            case "forward":
                                HorizontalPosition += distance;
                                Depth += Aim * distance;
                                break;
                            case "down":
                                Aim += distance;
                                break;
                            case "up":
                                Aim -= distance;
                                break;

                        }

                    }
                }

                Console.WriteLine($"F:{HorizontalPosition} D:{Depth} Part 2:{HorizontalPosition * Depth}");
            }
            Console.ReadLine();
        }
    }
}
