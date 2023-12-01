using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code
{
    public class day2
    {
        private enum PlayerMove
        {
            Rock = 'X',
            Paper = 'Y',
            Scissors = 'Z',
        }

        private enum EnemyMove
        {
            Rock = 'A',
            Paper = 'B',
            Scissors = 'C',
        }

        private Dictionary<char, char> losingMoves = new Dictionary<char, char>() 
        {
            // move : anti-move

            // Enemy losing moves
            {(char) EnemyMove.Rock,      (char) PlayerMove.Paper},
            {(char) EnemyMove.Paper,     (char) PlayerMove.Scissors},
            {(char) EnemyMove.Scissors,  (char) PlayerMove.Rock},

            // Our losing moves
            {(char) PlayerMove.Rock,     (char) EnemyMove.Paper},
            {(char) PlayerMove.Paper,    (char) EnemyMove.Scissors},
            {(char) PlayerMove.Scissors, (char) EnemyMove.Rock},
        };
        
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 2 - Part 1:");
            string input = File.ReadAllText("-----------Days/Day2/input.txt");
            string[] moves = input.Split("\n");
            int ourTotalScore = 0;

            foreach (string move in moves)
            {
                char opponentMove = move[0];
                char myMove = move[2];
                ourTotalScore += StartGame(myMove, opponentMove, false);
            }
            Console.WriteLine(ourTotalScore);
#endregion

#region Part 2
            Console.WriteLine("Day 2 - Part 2:");
            ourTotalScore = 0;
            
            foreach (string move in moves)
            {
                char opponentMove = move[0];
                char myMove = move[2];
                ourTotalScore += StartGame(myMove, opponentMove, true);
            }
            Console.WriteLine(ourTotalScore);
#endregion
        }

        private int StartGame(char myMove, char opponentMove, bool pt2) // return score
        {
            int currScore = 0;

#region Part 1
            if (!pt2)
            {
                if (losingMoves[myMove] == opponentMove) // we lost
                {
                    currScore += 0;
                }
                else if (losingMoves[opponentMove] == myMove) // we won
                {
                    currScore += 6;
                }
                else // draw
                {
                    currScore += 3;
                }
            }
#endregion

#region Part 2
            if (pt2)
            {
                if (myMove == 'X') // lose on purpose
                {
                    switch ((EnemyMove) opponentMove)
                    {
                        case EnemyMove.Rock:
                            myMove = (char) PlayerMove.Scissors;
                            break;
                        case EnemyMove.Paper:
                            myMove = (char) PlayerMove.Rock;
                            break;
                        case EnemyMove.Scissors:
                            myMove = (char) PlayerMove.Paper;
                            break;
                    }
                }
                else if (myMove == 'Y') // draw on purpose
                {
                    switch ((EnemyMove) opponentMove)
                    {
                        case EnemyMove.Rock:
                            myMove = (char) PlayerMove.Rock;
                            break;
                        case EnemyMove.Paper:
                            myMove = (char) PlayerMove.Paper;
                            break;
                        case EnemyMove.Scissors:
                            myMove = (char) PlayerMove.Scissors;
                            break;
                    }
                    currScore += 3;
                }
                else if (myMove == 'Z') // win on purpose
                {
                    myMove = losingMoves[opponentMove];
                    currScore += 6;
                }
            }
#endregion

            switch ((PlayerMove) myMove) // add default score from moves
            {
                case PlayerMove.Rock:
                    currScore += 1;
                    break;
                case PlayerMove.Paper:
                    currScore += 2;
                    break;
                case PlayerMove.Scissors:
                    currScore += 3;
                    break;
            }

            return currScore;
        }
    }
}