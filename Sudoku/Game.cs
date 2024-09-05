using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Game
{
    public Game()
    {
        this.Nodes = new List<Node>();
    }

    public Game(int[] initialNumbers)
    {
        Initialize(initialNumbers);
    }

    public void Initialize(int[] initialNumbers)
    {
        if (initialNumbers == null || initialNumbers.Length != 81)
            throw new Exception("A sudoku game has 9 rows, 9 columns");

        this.Nodes = new List<Node>();
        for (int i = 0; i < 81; i++)
        {
            this.Nodes.Add(new Node
            {
                Game = this,
                Index = i,
                PossibleNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            });
        }

        for (int i = 0; i < 81; i++)
        {
            if (initialNumbers[i] > 0)
            {
                this.Nodes.First(n => n.Index == i).Number = initialNumbers[i];
            }
        }
    }

    public int Id { get; set; }

    public List<Node> Nodes { get; set; }

    public void Print(string logFileName = "")
    {
        if (Nodes.Count != 81) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
        for (int i = 0; i < 9; i++)
        {
            if (Nodes.Count(n => n.Row == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
            if (Nodes.Count(n => n.Column == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
            if (Nodes.Count(n => n.Zone == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
        }

        if (!string.IsNullOrEmpty(logFileName)) Utils.InitLog(logFileName);

        string horizontalLine = "";
        for (int i = 0; i < 60; i++) horizontalLine = horizontalLine + "-";

        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0)
            {
                if (String.IsNullOrEmpty(logFileName))
                    Console.WriteLine(horizontalLine);
                else
                    Utils.LogWriteLine(horizontalLine, logFileName);
            }

            if (String.IsNullOrEmpty(logFileName))
                Console.WriteLine(horizontalLine);
            else
                Utils.LogWriteLine(horizontalLine, logFileName);

            // display a row - 3 lines, each line is 1/3 of 9 columns
            for (int lineNumber = 0; lineNumber < 3; lineNumber++)
            {
                string line = "";
                for (int column = 0; column < 9; column++)
                {
                    if (column % 3 == 0)
                    {
                        line = line + "|";
                    }

                    line = line + "|";

                    Node n = Nodes.First(n => n.Row == row && n.Column == column);
                    if (n.Number.HasValue)
                    {
                        string s = lineNumber == 1 ? "  " + n.Number.Value.ToString() + "  " : "     ";
                        line = line + s;
                    }
                    else
                    {
                        string numberString = "";
                        for (int i = 0; i < 3 && i + 3 * lineNumber < n.PossibleNumbers.Count; i++)
                        {
                            numberString = numberString + n.PossibleNumbers[i + 3 * lineNumber].ToString();
                        }
                        line = line + " " + numberString;
                        for (int x = 0; x < 4 - numberString.Length; x++)
                            line = line + " ";
                    }
                }

                line = line + "||";
                if (string.IsNullOrEmpty(logFileName))
                    Console.WriteLine(line);
                else
                    Utils.LogWriteLine(line, logFileName);
                line = "";
            }
        }


        if (String.IsNullOrEmpty(logFileName))
        {
            Console.WriteLine(horizontalLine);
            Console.WriteLine(horizontalLine);
        }
        else
        {
            Utils.LogWriteLine(horizontalLine, logFileName);
            Utils.LogWriteLine(horizontalLine, logFileName);
        }
    }

    public int Solve()
    {
        int prevPossibleNumbersCount = 729;
        int possibleNumbersCount = this.Nodes.Sum(n => n.PossibleNumbers.Count);

        while (prevPossibleNumbersCount > possibleNumbersCount)
        {
            prevPossibleNumbersCount = possibleNumbersCount;
            Node[] unsolvedNodes = this.Nodes.Where(n => !n.Number.HasValue).ToArray();
            Node node;

            for (int i = 0; i < unsolvedNodes.Length; i++)
            {
                node = unsolvedNodes[i];

                if (node.PossibleNumbers.Count == 1)
                {
                    node.Number = node.PossibleNumbers[0];
                    continue;
                }

                foreach (int number in node.PossibleNumbers)
                {
                    int setnumber = 0;
                    if (!unsolvedNodes.Any(n => n.Index != node.Index && n.Row == node.Row && n.PossibleNumbers.Contains(number)))
                    {
                        // no other node in the same row can use this number
                        setnumber = 1;
                    }
                    else if (!unsolvedNodes.Any(n => n.Index != node.Index && n.Column == node.Column && n.PossibleNumbers.Contains(number)))
                    {
                        // no other node in the same column can use this number
                        setnumber = 2;
                    }
                    else if (!unsolvedNodes.Any(n => n.Index != node.Index && n.Zone == node.Zone && n.PossibleNumbers.Contains(number)))
                    {
                        // no other node in the same row can use this number
                        setnumber = 3;
                    }

                    if (setnumber > 0)
                    {
                        node.Number = number;
                        break;
                    }

                }
            }


            List<Node> earlyTwinNodeList = new List<Node>();
            List<Node> laterTwinNodeList = new List<Node>();
            List<int> pairModeList = new List<int>();

            foreach (Node n0 in this.Nodes.Where(nn => nn.PossibleNumbers.Count == 2).ToList())
            {
                Node n1 = this.Nodes.FirstOrDefault(nn =>
                        nn.Index > n0.Index && nn.Row == n0.Row && nn.PossibleNumbers.Count == 2
                        && nn.PossibleNumbers[0] == n0.PossibleNumbers[0]
                        && nn.PossibleNumbers[1] == n0.PossibleNumbers[1]
                        );
                if (n1 != null)
                {
                    earlyTwinNodeList.Add(n0);
                    laterTwinNodeList.Add(n1);
                    pairModeList.Add(1);
                }

                Node n2 = this.Nodes.FirstOrDefault(nn =>
                    nn.Index > n0.Index && nn.Column == n0.Column && nn.PossibleNumbers.Count == 2
                    && nn.PossibleNumbers[0] == n0.PossibleNumbers[0]
                    && nn.PossibleNumbers[1] == n0.PossibleNumbers[1]
                    );
                if (n2 != null)
                {
                    earlyTwinNodeList.Add(n0);

                    laterTwinNodeList.Add(n2);
                    pairModeList.Add(2);
                }

                Node n3 = this.Nodes.FirstOrDefault(nn =>
                nn.Index > n0.Index && nn.Zone == n0.Zone && nn.PossibleNumbers.Count == 2
                && nn.PossibleNumbers[0] == n0.PossibleNumbers[0]
                && nn.PossibleNumbers[1] == n0.PossibleNumbers[1]
                );
                if (n3 != null)
                {
                    earlyTwinNodeList.Add(n0);

                    laterTwinNodeList.Add(n3);
                    pairModeList.Add(3);
                }


            }

            for (int i = 0; i < earlyTwinNodeList.Count; i++)
            {
                Node n1 = laterTwinNodeList[i];
                if (n1 == null) continue;
                Node n0 = earlyTwinNodeList[i];
                int pairMode = pairModeList[i];

                if (n0.PossibleNumbers.Count == 2)
                {
                    foreach (Node n in Nodes.Where(nn =>
                                                    nn.Index != n0.Index && nn.Index != n1.Index
                                                    && (pairMode == 1 ? nn.Row == n0.Row : (pairMode == 2 ? nn.Column == n0.Column : nn.Zone == n0.Zone)
                            )))
                    {
                        n.PossibleNumbers.Remove(n0.PossibleNumbers[0]);
                        n.PossibleNumbers.Remove(n0.PossibleNumbers[1]);
                    }
                }
            }

            possibleNumbersCount = this.Nodes.Sum(n => n.PossibleNumbers.Count);
        }

        return possibleNumbersCount > 0 ? -1 : 1;
    }
}