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
            int[] bits = null;
            //Part 1
            BitCount(File.ReadLines("puzzleinput.txt"), out bits);
            long gamma = 0;
            long epsilong = 0;
            for (int i = 0; i < bits.Length; ++i)
            {
                if (bits[i] > 0)
                {
                    gamma = gamma << 1 | 1;
                    epsilong = epsilong << 1;
                }
                else
                {
                    gamma = gamma << 1;
                    epsilong = epsilong << 1 | 1;
                }
            }

            Console.WriteLine($"Part 1: {gamma*epsilong}");
            //Part 2
            List<string> o2Lines = File.ReadLines("puzzleinput.txt").ToList();
            List<string> co2Lines = File.ReadLines("puzzleinput.txt").ToList();
            int pos = 0;
            do
            {
                
                int c = 0;
                foreach (var line in o2Lines)
                {
                    if (line[pos] == '0')
                        c--;
                    else
                        c++;
                }
                o2Lines.RemoveAll(t => t[pos] == ((c < 0) ? '1' : '0'));
                pos++;
            }
            while (o2Lines.Count > 1);

            pos = 0;
            do
            {
                int c = 0;
                foreach (var line in co2Lines)
                {
                    if (line[pos] == '0')
                        c--;
                    else
                        c++;
                }
                co2Lines.RemoveAll(t => t[pos] == ((c < 0) ? '0' : '1'));
                pos++;
            }
            while (co2Lines.Count > 1);

            Console.WriteLine($"Part 2: {Convert.ToUInt64(o2Lines.First(), 2) * Convert.ToUInt64(co2Lines.First(), 2)}");
            Console.ReadLine();

        }


        static void BitCount(IEnumerable<string> lines, out int[] bits)
        {
            bits = null;
            foreach (var line in lines)
            {
                if (bits == null)
                {
                    bits = new int[line.Length];
                }
                for (int i = 0; i < line.Length; ++i)
                {
                    if (line[i] == '0')
                        bits[i]--;
                    else
                        bits[i]++;
                }
            }
        }
    }
}
