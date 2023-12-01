using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code
{
    public class day8
    {
        private List<int> scenicScores = new List<int>();

        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 8 - Part 1:");
            string[] input = File.ReadAllLines("-----------Days/Day8/input.txt");

            int cols = input[0].Length; // get length of input
            int rows = input.Length; // get height of input
            int[,] trees = new int[rows, cols]; // make 2d array using sizes of input

            GetAllTrees(ref trees, input);
            Console.WriteLine(GetVisibleTrees(trees)); // get amount of visible trees
#endregion

#region Part 2
            Console.WriteLine("Day 8 - Part 2:");

            scenicScores.Sort((a, b) => {
                return b - a;
            }); // sort descending to get highest scenic score easily

            Console.WriteLine(scenicScores[0]); // get highest scenic score
#endregion
        }

        private int GetVisibleTrees(int[,] trees)
        {
            int visibleTrees = 0;
            for (int row = 1; row < trees.GetLength(0) - 1; row++)
            {
                for (int col = 1; col < trees.GetLength(1) - 1; col++)
                {
                    // Check every direction for trees:
                    (bool checkUp, int upTrees) = CheckUL(trees, row, col, true);
                    (bool checkLeft, int leftTrees) = CheckUL(trees, row, col, false);
                    (bool checkDown, int downTrees) = CheckDR(trees, row, col, true);
                    (bool checkRight, int rightTrees) = CheckDR(trees, row, col, false);

                    scenicScores.Add(upTrees * leftTrees * downTrees * rightTrees); // add the scenic score

                    if (checkUp || checkLeft || checkDown || checkRight)
                    {
                        visibleTrees++; // this tree is visible
                    }
                }
            }

            int perimeter = (trees.GetLength(1) * 2) + (trees.GetLength(0) * 2) - 4; // add all edges, since they are visible trees
            return visibleTrees + perimeter;
        }

        private (bool pass, int treesFound) CheckDR(int[,] trees, int currRow, int currCol, bool checkRow) // able to check down or right
        {
            int treesFound = 0;
            for (int i = (checkRow ? currRow : currCol) + 1; i < trees.GetLength(checkRow ? 0 : 1); i++)
            {
                treesFound++;
                if (trees[currRow, currCol] <= (checkRow ? trees[i, currCol] : trees[currRow, i]))
                {
                    return (false, treesFound);
                }
            }

            return (true, treesFound);
        }

        private (bool pass, int treesFound) CheckUL(int[,] trees, int currRow, int currCol, bool checkRow) // able to check up or left
        {
            int treesFound = 0;
            for (int i = (checkRow ? currRow : currCol) - 1; i >= 0; i--)
            {
                treesFound++;
                if (trees[currRow, currCol] <= (checkRow ? trees[i, currCol] : trees[currRow, i]))
                {
                    return (false, treesFound);
                }
            }
            
            return (true, treesFound);
        }

        private void GetAllTrees(ref int[,] trees, string[] input)
        {
            for (int row = 0; row < trees.GetLength(0); row++)
            {
                for (int col = 0; col < trees.GetLength(1); col++)
                {
                    trees[row, col] = (int) char.GetNumericValue(input[row][col]); // import trees from input to the 2d array
                }
            }
        }
    }
}
