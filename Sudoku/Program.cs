using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EulerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int[]> sampleGames = ReadSampleData();

            for(int i = 6; i < 7; i ++)
            {
                Game game = new Game(sampleGames[i]);
                game.Id = i;
                game.Print($"game{i}_log00.txt");

                int solved = game.Solve();
                string msg = solved < 0 ? $"Game {i} cannot be solved by easy elimination. ({game.Nodes.Sum(n => n.PossibleNumbers.Count)})" : $"Game {i} is solved.";
                Console.WriteLine(msg);

                game.Print($"game{i}_log01.txt");

                Console.WriteLine("Step by step:");
                int endResult = game.SolveStepByStep();

                Console.WriteLine(endResult == 1 ? "Game is solved." : "Cannot solve the game.");
            }
        }

        static List<int[]> ReadSampleData()
        {

            List<int[]> gameList = new List<int[]>();
            System.IO.StreamReader sr = new System.IO.StreamReader("p096_sudoku.txt");
            string line;
            List<int> numbers=new List<int>();
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Grid"))
                {
                    if (numbers.Count > 0) gameList.Add(numbers.ToArray());
                    numbers = new List<int>();
                }
                else
                {
                    for (int c = 0; c < 9; c++)
                    {
                        int n = line[c] - '0';
                        numbers.Add(n);
                    }
                }
            }

            sr.Close();

            if (numbers != null && numbers.Count > 0) gameList.Add(numbers.ToArray());

            return gameList;
        }
    }
}