using System;
using System.Collections.Generic;
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
                            Solved = (n != 0),
                            Zone = row / 3 * 3 + c / 3,
                            Number = n,
                            PossibleNumbers = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9,}
                        };
                        node.PossibleNumbers.Remove(n);
                        allNodes.Add(node);
                    }
                    row ++;
                }
            }

            sr.Close();

            foreach(Node n in allNodes)
            {
                if (n.Solved) n.PossibleNumbers.Clear();
            }



            SolveGame(1);

            return "";
        }

        void PrintGame(int gameId, bool printtolog=false)
        {
            List<Node> nodes= allNodes.Where(n => n.GameId == gameId).ToList();
            if (nodes.Count != 81) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");

            for(int r = 0; r < 9; r ++)
            {
                if (r % 3== 0) Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("--------------------------------------------------");

                if (printtolog)
                {
                    if (r % 3== 0) LogWriteLine("--------------------------------------------------");
                    LogWriteLine("--------------------------------------------------");
                }

                for(int x = 0; x < 3; x ++)
                {
                    for(int c = 0; c < 9; c++)
                    {
                        if (c % 3 == 0) Console.Write("|");
                        if (printtolog) 
                        {
                            if (c % 3 == 0) LogWrite("|");
                        }

                        Node n = nodes.FirstOrDefault(n => n.Row == r && n.Column == c);
                        if (n.PossibleNumbers.Count > 0)
                        {
                            Console.Write(" ");
                            for(int i = 3 * x; i < 3 + 3 * x && i < n.PossibleNumbers.Count; i ++) Console.Write(n.PossibleNumbers[i]);
                            Console.Write(" ");

                            if (printtolog)
                            {
                                LogWrite(" ");
                                for(int i = 3 * x; i < 3 + 3 * x && i < n.PossibleNumbers.Count; i ++) LogWrite(n.PossibleNumbers[i].ToString());
                                LogWrite(" ");
                            }
                        }
                        else if (x == 1)
                        {
                            Console.Write($"  {n.Number}  ");
                            if (printtolog)
                            {
                                LogWrite($"  {n.Number}  ");
                            }
                        }
                        else
                        {
                            Console.Write("     ");
                            if (printtolog)
                            {
                                LogWrite("     ");
                            }
                        }
                            
                    }

                    Console.WriteLine("");
                    if (printtolog)
                    {
                        LogWriteLine("");
                    }
                }   
                if (r % 3 > 0) Console.WriteLine("");  
                if (printtolog)
                {
                    if (r % 3 > 0) LogWriteLine("");  
                }
            }
            Console.WriteLine("---------------------------------------------");
            if (printtolog)
            {
                LogWriteLine("---------------------------------------------");
            }
        }

        void InitLog()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("log.txt", false);
            sw.Close();
        }

        void LogWrite(string msg)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("log.txt", true);
            sw.Write(msg);
            sw.Close();
        }

        void LogWriteLine(string msg)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("log.txt", true);
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


            PrintGame(nodes[0].GameId, true);

            int possibleNumbersCount = nodes.Sum(n => n.PossibleNumbers.Count);
            int prevPossibleNumbersCount = 81 * 9;

            while (possibleNumbersCount != prevPossibleNumbersCount && nodes.Any(n => !n.Solved))
            {
                prevPossibleNumbersCount = possibleNumbersCount;

                foreach(Node solvedNode in nodes.Where(n => n.Solved))
                {
                    foreach(Node n in nodes.Where(nn => 
                        nn.Row == solvedNode.Row
                        || nn.Column == solvedNode.Column
                        || nn.Zone == solvedNode.Zone)) 
                        n.PossibleNumbers.Remove(solvedNode.Number);
                }

                foreach(Node n in nodes.Where(nn => !nn.Solved && nn.PossibleNumbers.Count() == 1)) 
                {
                    n.Solved = true;
                    n.PossibleNumbers.Clear();
                }

                Console.WriteLine("After removing solved numbers from all possible numbers list");
                PrintGame(nodes[0].GameId, true);

                List<Node> unsolvedNodes = nodes.Where(nn => !nn.Solved).ToList();
                for(int i = 0; i < unsolvedNodes.Count; i ++)
                {
                    Node n = unsolvedNodes[i];
                    foreach(int j in n.PossibleNumbers)
                    {
                        if (
                            !nodes.Any(nn => nn.PossibleNumbers.Contains(j) && nn.Row == n.Row && nn.Column!=n.Column)
                            || !nodes.Any(nn => nn.PossibleNumbers.Contains(j) && nn.Zone == n.Zone && (nn.Column!=n.Column || nn.Row != n.Row))
                            || !nodes.Any(nn => nn.PossibleNumbers.Contains(j) && nn.Row != n.Row && nn.Column==n.Column)
                            )
                        {
                            n.Solved = true;
                            n.Number = j;
                            n.PossibleNumbers.Clear();
                            break;
                        }
                    }
                }

                Console.WriteLine("After setting node with exclusive possible number to solved");
                PrintGame(nodes[0].GameId, true);
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

            // SingleEliminate(nodes);
        }
    }

    public class Node 
    {
        public int GameId {get;set;}

        public int Row{get;set;}

        public int Column{get;set;}

        public int Zone{get;set;}

        public bool Solved{get;set; }

        public int Number{get;set;} 

        public List<int> PossibleNumbers{get;set;}
    }
}
