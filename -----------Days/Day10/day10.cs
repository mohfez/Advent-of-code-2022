using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code
{
    public class day10
    {
        public List<Instruction> instructions = new List<Instruction>();

        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 10 - Part 1:");
            string[] input = File.ReadAllLines("-----------Days/Day10/input.txt");

            ReadInstructions(input);
            Console.WriteLine(GetSumSignalStrength());
#endregion

#region Part 2
            Console.WriteLine("Day 10 - Part 2:");
            DrawCRT();
#endregion
        }

        private void DrawCRT()
        {
            int currentCycle = 0;
            int spritePosition = 1; // sprite position = register
            foreach (Instruction instruction in instructions)
            {
                for (int cycle = 0; cycle < instruction.cycles; cycle++)
                {
                    currentCycle++;
                    int xAxis = (currentCycle - 1) % 40; // get current x position
                    if (xAxis >= spritePosition - 1 && xAxis <= spritePosition + 1) // check if sprite is in the way
                    {
                        Console.Write("⬜");
                    }
                    else
                    {
                        Console.Write("⬛");
                    }

                    if ((xAxis + 1) % 40 == 0) Console.Write("\n"); // add a new line if we are finished with this line
                }
                spritePosition += instruction.parameter; // add to register (sprite position)
            }
        }

        private int GetSumSignalStrength()
        {
            int currentCycle = 0;
            int registerX = 1;
            int sum = 0;
            foreach (Instruction instruction in instructions)
            {
                for (int cycle = 0; cycle < instruction.cycles; cycle++)
                {
                    currentCycle++;
                    if (currentCycle % 40 == 20) // check if cycle started at 20 and is going up in 40s
                    {
                        sum += currentCycle * registerX; // find sum
                    }
                }
                registerX += instruction.parameter; // add to register
            }

            return sum;
        }

        private void ReadInstructions(string[] input)
        {
            foreach (string line in input)
            {
                string[] commands = line.Split(" ");

                // make new object and add it to the instructions list
                Instruction instruction = new Instruction(GetCycleAmount(commands[0]), commands[0], commands.Length == 2 ? int.Parse(commands[1]) : 0);
                instructions.Add(instruction);
            }
        }

        private int GetCycleAmount(string cmd)
        {
            int cycles = 0;
            switch (cmd) // define how many cycles a command has
            {
                case "noop":
                    cycles = 1;
                    break;
                case "addx":
                    cycles = 2;
                    break;
            }

            return cycles;
        }

        public class Instruction
        {
            public int cycles = 1;
            public string cmd = "noop";
            public int parameter = 0;

            public Instruction(int cycles, string cmd, int parameter)
            {
                this.cycles = cycles;
                this.cmd = cmd;
                this.parameter = parameter;
            }
        }
    }
}