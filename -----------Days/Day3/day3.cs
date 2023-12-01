using System;
using System.IO;

namespace advent_of_code
{
    public class day3
    {
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 3 - Part 1:");
            string input = File.ReadAllText("-----------Days/Day3/input.txt");
            string[] rucksacks = input.Split("\n");
            int sumOfPriorities = 0;

            foreach (string rucksack in rucksacks)
            {
                string firstHalf = rucksack.Substring(0, rucksack.Length / 2);
                string secondHalf = rucksack.Substring(rucksack.Length / 2, rucksack.Length / 2);

                sumOfPriorities += CompareRucksacks(firstHalf, secondHalf);
            }
            Console.WriteLine(sumOfPriorities);
#endregion

#region Part 2
            Console.WriteLine("Day 3 - Part 2:");
            sumOfPriorities = 0;
            for (int i = 2; i < rucksacks.Length; i += 3)
            {
                string r1 = rucksacks[i - 2];
                string r2 = rucksacks[i - 1];
                string r3 = rucksacks[i];

                sumOfPriorities += CompareGroupRucksacks(r1, r2, r3);
            }
            Console.WriteLine(sumOfPriorities);
#endregion
        }

        private int CompareRucksacks(string r1, string r2) // return sum of priorities
        {
            int total = 0;
            for (int i = 0; i < r1.Length; i++)
            {
                char currChar = r1[i];

                if (r2.Contains(currChar))
                {
                    total += GetPriority(currChar);
                    break; // exit loop
                }
            }

            return total;
        }

        private int CompareGroupRucksacks(string r1, string r2, string r3) // return sum of priorities
        {
            int total = 0;
            for (int i = 0; i < r1.Length; i++)
            {
                char currChar = r1[i];

                if (r2.Contains(currChar) && r3.Contains(currChar))
                {
                    total += GetPriority(currChar);
                    break; // exit loop
                }
            }

            return total;
        }

        private int GetPriority(char c)
        {
            if (char.IsUpper(c))
            {
                return (int)c - 38;
            }
            else
            {
                return (int)c - 96;
            }
        }
    }
}