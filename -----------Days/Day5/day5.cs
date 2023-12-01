using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace advent_of_code
{
    public class day5
    {
        private const int CratesEndingLine = 8; // how many lines do the crates take up in the input file?
        private const int CrateStackAmount = 9; // how many stacks of crates are there?
        private const int MovesStartingLine = 11; // on what line do the instructions start at?

        private List<string>[] crates = new List<string>[CrateStackAmount]; 

        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 5 - Part 1:");
            string input = File.ReadAllText("-----------Days/Day5/input.txt");
            string[] lines = input.Split("\n");

            GetCrates(lines);
            RearrangeCrates(lines, false);
#endregion

#region Part 2
            Console.WriteLine("Day 5 - Part 2:");
            GetCrates(lines); // reset list
            RearrangeCrates(lines, true);
#endregion
        }

        private void GetCrates(string[] lines)
        {
            for (int i = CratesEndingLine - 1; i >= 0; i--)
            {
                int currCrateList = 0;
                string cratesLine = lines[i];
                for (int j = 1; j < cratesLine.Length; j += 4)
                {
                    if (crates[currCrateList] == null)
                    {
                        crates[currCrateList] = new List<string>();
                    }
                    if (cratesLine[j] != ' ') crates[currCrateList].Add("[" + cratesLine[j] + "]");

                    currCrateList++;
                    if (currCrateList > CrateStackAmount)
                    {
                        currCrateList = 0;
                    }
                }
            }
        }

        private void RearrangeCrates(string[] lines, bool sorted)
        {
            for (int i = MovesStartingLine - 1; i < lines.Length; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], @"\d+");
                                
                int amountOfMoves = int.Parse(matches[0].Value);
                int list1Index = int.Parse(matches[1].Value);
                int list2Index = int.Parse(matches[2].Value);
               
                List<string> firstList = crates[list1Index - 1];
                List<string> secondList = crates[list2Index - 1];
                int whereToBegin = firstList.Count - amountOfMoves;

                if (sorted)
                {
                    while (whereToBegin != firstList.Count)
                    {
                        string currElement = firstList[whereToBegin];
                        firstList.RemoveAt(whereToBegin);
                        secondList.Add(currElement);
                    }
                }
                else
                {
                    for (int j = firstList.Count - 1; j >= whereToBegin; j--)
                    {
                        string currElement = firstList[j];
                        firstList.RemoveAt(j);
                        secondList.Add(currElement);
                    }
                }
            }
            
            for (int i = 0; i < crates.Length; i++) // display latest crates
            {
                Console.WriteLine(crates[i][crates[i].Count - 1]);
            }
        }
    }
}