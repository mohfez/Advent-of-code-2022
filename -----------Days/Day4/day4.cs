using System;
using System.IO;

namespace advent_of_code
{
    public class day4
    {
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 4 - Part 1:");
            string input = File.ReadAllText("-----------Days/Day4/input.txt").Replace(",", "-"); // replace all commas to make things easier
            string[] pairs = input.Split("\n");
            int occurrences = 0;

            foreach (string pair in pairs)
            {
                string[] numbers = pair.Split("-");
                occurrences += CheckFullyContains(numbers) ? 1 : 0;
            }
            Console.WriteLine(occurrences);
#endregion

#region Part 2
            Console.WriteLine("Day 4 - Part 2:");
            occurrences = 0;
            foreach (string pair in pairs)
            {
                string[] numbers = pair.Split("-");
                occurrences += CheckRangeOverlap(numbers) ? 1 : 0;
            }
            Console.WriteLine(occurrences);
#endregion
        }

        private bool CheckFullyContains(string[] numbers)
        {
            int[] elf1 = { int.Parse(numbers[0]), int.Parse(numbers[1]) };
            int[] elf2 = { int.Parse(numbers[2]), int.Parse(numbers[3]) };

            if (elf1[0] <= elf2[0] && elf1[1] >= elf2[1])
            {
                return true;
            }
            else if (elf2[0] <= elf1[0] && elf2[1] >= elf1[1])
            {
                return true;
            }

            return false;
        }

        private bool CheckRangeOverlap(string[] numbers)
        {
            int[] elf1 = { int.Parse(numbers[0]), int.Parse(numbers[1]) };
            int[] elf2 = { int.Parse(numbers[2]), int.Parse(numbers[3]) };

            if (elf1[0] <= elf2[0] && elf1[1] >= elf2[0])
            {
                return true;
            }
            else if (elf2[0] <= elf1[0] && elf2[1] >= elf1[0])
            {
                return true;
            }

            return false;
        }
    }
}