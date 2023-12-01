using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code
{
    public class day1
    {
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 1 - Part 1:");
            string input = File.ReadAllText("-----------Days/Day1/input.txt");
            string[] elfSections = input.Split("\n\n");
            List<int> totals = new List<int>();
            
            foreach (string elves in elfSections)
            {
                int total = 0;
                string[] elf = elves.Split("\n");
                for (int i = 0; i < elf.Length; i++)
                {
                    total += int.Parse(elf[i]);
                }
                totals.Add(total);
            }

            totals.Sort((a, b) => {
                return b - a;
            }); // sort list DESC
            Console.WriteLine(totals[0]);
#endregion

#region Part 2
            Console.WriteLine("Day 1 - Part 2:");
            Console.WriteLine(totals[0] + totals[1] + totals[2]);
#endregion
        }
    }
}