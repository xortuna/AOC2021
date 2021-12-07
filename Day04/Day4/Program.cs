using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        enum ReadState {
            BingoBalls,
            WaitForCard,
            ProcessCard,
        }

        class BingoNumber
        {
            public int Number;
            public bool Seen = false;

            public BingoNumber(int n)
            {
                Number = n;
            }
        }

        class BingoCard
        {
            public List<List<BingoNumber>> GridState = new List<List<BingoNumber>>();

            public bool Mark(int number)
            {
                foreach(var row in GridState)
                {
                    foreach(var col in row)
                    {
                        if (col.Number == number) 
                            col.Seen = true;
                    }
                }
                return HasBingo();
            }

            public bool HasBingo()
            {
                //row bingo
                foreach(var row in GridState)
                {
                    if (row.All(t => t.Seen))
                        return true;
                }

                //Column bingo
                for(int i =0; i < GridState[0].Count; ++i)
                {
                    if (GridState.All(t => t[i].Seen))
                        return true;
                }
                return false;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<int> bingoNumbers = new List<int>();
            List<BingoCard> cards = new List<BingoCard>();
            ReadState state = ReadState.BingoBalls;
            
            //Part 1
            using (StreamReader sr = new StreamReader("puzzleinput.txt"))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {

                    switch (state)
                    {
                        case ReadState.BingoBalls:
                            bingoNumbers = line.Split(",").Select(t=>int.Parse(t)).ToList();
                            state = ReadState.WaitForCard;
                            break;
                        case ReadState.WaitForCard:
                            if (string.IsNullOrEmpty(line))
                                continue;

                            cards.Add(new BingoCard());
                            cards.Last().GridState.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(t => new BingoNumber(int.Parse(t))).ToList());
                            state = ReadState.ProcessCard;
                            break;
                        case ReadState.ProcessCard:
                            if (string.IsNullOrEmpty(line))
                            {
                                state = ReadState.WaitForCard;
                                continue;
                            }

                            cards.Last().GridState.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(t => new BingoNumber(int.Parse(t))).ToList());
                            break;
                    }
                }

            }
            bool donePart1 = false;
            foreach(var ball in bingoNumbers)
            {
                foreach(var card in cards)
                {
                    if (card.Mark(ball))
                    {
                        if (!donePart1)
                        {
                            OutputPuzzleAnswer("Part 1", card, ball);
                            donePart1 = true;
                        }
                        if(cards.All(t => t.HasBingo()))
                        {
                            OutputPuzzleAnswer("Part 2", card, ball);
                            goto exit;
                        }

                    }
                }            
            }
            exit:
            Console.ReadLine();
        }

        private static void OutputPuzzleAnswer(string title, BingoCard card, int ball)
        {
            var totalUnseen = card.GridState.Sum(t => t.Where(r => !r.Seen).Sum(r => r.Number));

            Console.WriteLine($"{title} {totalUnseen} * {ball} = {totalUnseen*ball}");
        }
    }
}
