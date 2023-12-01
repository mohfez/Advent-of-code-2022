using System;
using System.IO;

namespace advent_of_code
{
    public class day6
    {
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 6 - Part 1:");
            string input = File.ReadAllText("-----------Days/Day6/input.txt");
            GetMarker(input, 4);
#endregion

#region Part 2
            Console.WriteLine("Day 6 - Part 2:");
            GetMarker(input, 14);
#endregion
        }

        // distintCharactersLen - how many unique characters to read
        private void GetMarker(string input, int distintCharactersLen)
        {
            string strToCheck = "";
            int marker = 0;
            while (marker < input.Length)
            {
                strToCheck = ""; // reset
                for (int i = 0; i < distintCharactersLen; i++)
                {
                    if (marker + i >= input.Length) // none found, end program
                    {
                        marker = input.Length + 1; // end while loop
                        break; // exit for loop
                    }

                    char charToAdd = input[marker + i];
                    if (strToCheck.Contains(charToAdd))
                    {
                        strToCheck = ""; // reset if there are duplicates
                        break;
                    }
                    else
                    {
                        strToCheck += charToAdd; // add if no duplicates
                    }
                }

                if (strToCheck != "") // finish up before ending while loop
                {
                    marker += distintCharactersLen;
                    break;
                }
                marker++;
            }

            Console.WriteLine(strToCheck + ":" + marker);
        }
    }
}