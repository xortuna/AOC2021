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
            using (StreamReader sr = new StreamReader("puzzleinput.txt"))
            {
                string line = null;
                int increase = 0;
                int? cache = null;
                while ((line = sr.ReadLine()) != null)
                {
                    var l = int.Parse(line);

                    if (cache.HasValue && cache.Value < l)
                        increase++;
                    cache = l;
                }

                Console.WriteLine($"Part 1: Increases {increase}");
                Console.ReadLine();
            }
            //Part 2
            using (StreamReader sr = new StreamReader("puzzleinput.txt"))
            {
                string line = null;
                int increase = 0;
                int? cache = null;
                Queue<int> rollingWindow = new Queue<int>();
                while ((line = sr.ReadLine()) != null)
                {
                    var l = int.Parse(line);
                    rollingWindow.Enqueue(l);
                    if (rollingWindow.Count >= 3)
                    {
                        if(rollingWindow.Count > 3)
                            rollingWindow.Dequeue();
                        var sum = rollingWindow.Sum();
                        if (cache.HasValue && cache.Value < sum)
                            increase++;
                        cache = sum;
                    }
                }

                Console.WriteLine($"Part 2: Increases {increase}");
                Console.ReadLine();
            }

        }
    }
}
