using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code
{
    public class day9
    {   
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 9 - Part 1:");
            string[] instructions = File.ReadAllLines("-----------Days/Day9/input.txt");
            Console.WriteLine(ReadInstructions(instructions, 2));
#endregion

#region Part 2
            Console.WriteLine("Day 9 - Part 2:");
            Console.WriteLine(ReadInstructions(instructions, 10));
#endregion
        }

        private int ReadInstructions(string[] instructions, int amount)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>(); // ignores duplicates automatically
            Knot[] knots = new Knot[amount];
            for(int i = 0; i < amount; i++) knots[i] = new Knot(0, 0); // fill array

            foreach (string command in instructions)
            {
                string[] instruction = command.Split(" ");
                for (int i = 0; i < int.Parse(instruction[1]); i++) // amount of steps parameters[1]
                {
                    switch ((Direction) instruction[0][0]) // get direction U, D, L, R
                    {
                        case Direction.Up:
                            knots[0].Move(0, 1);
                            break;
                        case Direction.Down:
                            knots[0].Move(0, -1);
                            break;
                        case Direction.Left:
                            knots[0].Move(-1, 0);
                            break;
                        case Direction.Right:
                            knots[0].Move(1, 0);
                            break;
                    }

                    for (int knotIndex = 1; knotIndex < knots.Length; knotIndex++)
                    {
                        knots[knotIndex].AttachTo(knots[knotIndex - 1]); // attach to previous knot
                    }
                    visited.Add((knots[knots.Length - 1].x, knots[knots.Length - 1].y)); // add final knot position
                }
            }
            
            return visited.Count;
        }

        private enum Direction
        {
            Up = 'U',
            Down = 'D',
            Left = 'L',
            Right = 'R'
        }

        private class Knot
        {
            public int x = 0;
            public int y = 0;

            public Knot(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public void Move(int x, int y)
            {
                this.x += x;
                this.y += y;
            }

            public void AttachTo(Knot other)
            {
                if (IsTouching(other)) return;

                if (other.x > this.x) Move(1, 0);
                if (other.x < this.x) Move(-1, 0);
                if (other.y > this.y) Move(0, 1);
                if (other.y < this.y) Move(0, -1);
            }

            public bool IsTouching(Knot other)
            {
                return Math.Max(Math.Abs(other.x - this.x), Math.Abs(other.y - this.y)) <= 1;
            }
        }
    }
}