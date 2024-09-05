using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem096 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return "problem 96 sudoku";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 96;
            }
        }

        List<Node> allNodes;

        public override string Solution1()
        {
            allNodes = new List<Node>();
            System.IO.StreamReader sr = new System.IO.StreamReader("Files/p096_sudoku.txt");
            string line;
            int gridId = 0;
            int row = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Grid"))
                {
                    gridId = Convert.ToInt32(line.Replace("Grid", "").Trim());
                    row = 0;
                }
                else
                {
                    for(int c = 0; c < 9; c++)
                    {
                        int n = line[c] - '0';
                        Node node = new Node{
                            Row = row,
                            GameId = gridId,
                            Column = c,
                            Zone = row / 3 * 3 + c / 3,
                            Number = n,
                            PossibleNumbers = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9,}
                        };

                        if (node.Number.Value == 0)
                        {
                            node.Number = null;
                        }
                        else
                        {
                            node.PossibleNumbers.Clear();
                        }
                        allNodes.Add(node);
                    }
                    row ++;
                }
            }

            sr.Close();

            SolveGame(1);

            return "";
        }

        void PrintGame(int gameId, string logFileName = "log.txt")
        {
            List<Node> nodes= allNodes.Where(n => n.GameId == gameId).ToList();
            if (nodes.Count != 81) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");

            string horizontalLine = "";
            for(int i = 0; i < 60; i ++) horizontalLine = horizontalLine + "-";

            for(int row = 0; row < 9; row ++)
            {
                if (row % 3 == 0)
                {
                    Console.WriteLine(horizontalLine);
                    LogWriteLine(horizontalLine, logFileName);
                }
                Console.WriteLine(horizontalLine);
                LogWriteLine(horizontalLine, logFileName);

                // display a row - 3 lines, each line is 1/3 of 9 columns
                for(int lineNumber = 0; lineNumber < 3; lineNumber ++)
                {
                    string line = "";
                    for(int column = 0; column < 9; column ++)
                    {
                        if (column % 3 == 0)
                        {
                            line = line + "|";
                        }

                        line = line + "|";

                        Node n = nodes.First(n => n.Row == row && n.Column == column);
                        if (n.Number.HasValue)
                        {
                            string s = lineNumber == 1 ? "  " + n.Number.Value.ToString() + "  " : "     ";
                            line = line + s;
                        }
                        else
                        {
                            string numberString = "";
                            for(int i = 0; i < 3 && i + 3 * lineNumber < n.PossibleNumbers.Count; i ++)
                            {
                                numberString = numberString + n.PossibleNumbers[i + 3 * lineNumber].ToString();
                            }
                            line = line + " " + numberString;
                            for(int x = 0; x < 4 - numberString.Length; x ++)
                            line = line + " ";
                        }
                    }
                    
                    line = line + "||";
                    Console.WriteLine(line);
                    LogWriteLine(line, logFileName);
                    line = "";
                }
            }

            Console.WriteLine(horizontalLine);
            LogWriteLine(horizontalLine, logFileName);
            Console.WriteLine(horizontalLine);
            LogWriteLine(horizontalLine, logFileName);
        }

        void InitLog(string fileName = "log.txt")
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false);
            sw.Close();
        }

        void LogWrite(string msg, string fileName = "log.txt")
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);
            sw.Write(msg);
            sw.Close();
        }

        void LogWriteLine(string msg, string fileName = "log.txt")
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);
            sw.WriteLine(msg);
            sw.Close();
        }

        List<Node> SingleEliminate(List<Node> nodes)
        {
            if (nodes.Count != 81) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
            for(int i = 0; i < 9; i ++)
            {
                if (nodes.Count(n => n.Row == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
                if (nodes.Count(n => n.Column == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
                if (nodes.Count(n => n.Zone == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
            }


            PrintGame(nodes[0].GameId);

            int possibleNumbersCount = nodes.Sum(n => n.PossibleNumbers.Count);
            int prevPossibleNumbersCount = 81 * 9;

            int step = 0;

            while (possibleNumbersCount != prevPossibleNumbersCount && nodes.Any(n => !n.Solved))
            {
                prevPossibleNumbersCount = possibleNumbersCount;

                foreach(Node solvedNode in nodes.Where(n => n.Solved))
                {
                    foreach(Node n in nodes.Where(nn => 
                        nn.Row == solvedNode.Row
                        || nn.Column == solvedNode.Column
                        || nn.Zone == solvedNode.Zone)) 
                        n.PossibleNumbers.Remove(solvedNode.Number.Value);
                }

                foreach(Node n in nodes.Where(nn => !nn.Solved && nn.PossibleNumbers.Count() == 1)) 
                {
                    n.Number = n.PossibleNumbers[0];
                    n.PossibleNumbers.Clear();
                }

                step ++;
                string logFileName = "Log" + step.ToString() + ".txt";
                InitLog(logFileName);

                Console.WriteLine($"After step {step} removing solved numbers from all possible numbers list");
                PrintGame(nodes[0].GameId, logFileName);

                List<Node> unsolvedNodes = nodes.Where(nn => !nn.Solved).ToList();
                for(int i = 0; i < unsolvedNodes.Count; i ++)
                {
                    Node n = unsolvedNodes[i];
                    if (n.Row == 2 && (n.Column <= 1))
                    {
                        int u = 9;
                    }

                    foreach(int j in n.PossibleNumbers)
                    {
                        int y = -1;
                        if (!nodes.Any(nn => nn.PossibleNumbers.Contains(j) && nn.Row == n.Row && nn.Column!=n.Column))
                            y = 0;
                        else if (!nodes.Any(nn => nn.PossibleNumbers.Contains(j) && nn.Zone == n.Zone && (nn.Column!=n.Column || nn.Row != n.Row)))
                            y = 1;
                        else if (!nodes.Any(nn => nn.PossibleNumbers.Contains(j) && nn.Row != n.Row && nn.Column==n.Column))
                            y = 2;
                        if (y > 0)
                        {
                            n.Number = j;
                            n.PossibleNumbers.Clear();
                            break;
                        }
                    }
                }

                step ++;
                Console.WriteLine($"After step {step} - setting node with exclusive possible number to solved");
                logFileName = "Log" + step.ToString() + ".txt";
                PrintGame(nodes[0].GameId, logFileName);
                possibleNumbersCount = nodes.Sum(n => n.PossibleNumbers.Count);
            } 

            
            if (!nodes.Any(n => !n.Solved))
                Console.WriteLine($"Game {nodes[0].GameId} is solved");
            else
                Console.WriteLine($"Game {nodes[0].GameId} cannot be solved only by single elimination");

            return new List<Node>(nodes);
        } 

        void SolveGame(int gameId)
        {
            List<Node> nodes= allNodes.Where(n => n.GameId == gameId).ToList();
            if (nodes.Count != 81) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");

            InitLog();

            SingleEliminate(nodes);
        }
    }

    public class Node 
    {
        public int GameId {get;set;}

        public int Row{get;set;}

        public int Column{get;set;}

        public int Zone{get;set;}

        public int Index 
        {
            get
            {
                return Row * 9 + Column;
            }
        }

        public bool Solved
        {
            get
            {
                return Number.HasValue;
            }
        }

        public int? Number{get;set;} 

        public List<int> PossibleNumbers{get;set;}

        public string[] Output
        {
            get
            {
                string [] threeLines = new string[3];

                for(int line = 0; line < 3; line ++)
                {
                    if(Number.HasValue)
                        threeLines[line] = line == 1 ? $"  {Number.Value}  " : "     ";
                    else
                    {
                        threeLines[line] = " ";
                        for(int i = 0; i < 3 && i + 3 * line < PossibleNumbers.Count; i++)
                            threeLines[line] = threeLines[line] + PossibleNumbers[3 * line + i];
                        threeLines[line] = threeLines[line] + " ";
                    }
                }

                return new string[3];
            }
        }
    }
}
