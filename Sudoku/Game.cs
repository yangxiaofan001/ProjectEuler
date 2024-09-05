using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters;

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

    public int CandidatesCount
    {
        get { return this.Nodes.Sum(n => n.PossibleNumbers.Count); }
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

    public int Tech01_NakedSingle()
    {
        Node nakedSingleNode = Nodes.FirstOrDefault(n => n.PossibleNumbers.Count == 1);
        if (nakedSingleNode != null)
        {
            nakedSingleNode.Number = nakedSingleNode.PossibleNumbers[0];
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public int Tech02_HiddenSingle()
    {
        for (int i = 1; i <= 9; i++)
        {
            for (int checkingType = 0; checkingType < 3; checkingType++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Node[] nodes = Nodes.Where(n => n.PossibleNumbers.Contains(i)
                    && (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == j).ToArray();
                    if (nodes.Length == 1)
                    {
                        nodes[0].Number = nodes[0].PossibleNumbers[0];
                        return 1;
                    }
                }
            }
        }

        return -1;
    }

    public int Tech03_NakedPair()
    {
        for (int checkingType = 0; checkingType < 3; checkingType++)
        {
            for (int checkingIndex = 0; checkingIndex < 9; checkingIndex++)
            {
                Node[] nodes = Nodes.Where(n => n.PossibleNumbers.Count == 2
                                && (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == checkingIndex).ToArray();

                for (int n1Index = 0; n1Index < nodes.Length - 1; n1Index++)
                {
                    for (int n2Index = n1Index + 1; n2Index < nodes.Length; n2Index++)
                    {
                        Node n1 = nodes[n1Index];
                        Node n2 = nodes[n2Index];

                        if (n1.PossibleNumbers[0] == n2.PossibleNumbers[0] && n1.PossibleNumbers[1] == n2.PossibleNumbers[1])
                        {
                            // a pair is found, now check if can eliminate anything
                            Node[] nodesWithCandidatesToBeRemoved =
                                Nodes.Where(n =>
                                        (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == (checkingType == 0 ? n1.Row : (checkingType == 1 ? n1.Column : n1.Zone))
                                        && n.Index != n1.Index && n.Index != n2.Index
                                        && (n.PossibleNumbers.Contains(n1.PossibleNumbers[0]) || n.PossibleNumbers.Contains(n1.PossibleNumbers[1]))
                                        ).ToArray();
                            if (nodesWithCandidatesToBeRemoved.Length > 0)
                            {
                                // there are candidates to be removed in other node in the same row/column/zone
                                foreach (Node n in nodesWithCandidatesToBeRemoved)
                                {
                                    n.PossibleNumbers.Contains(n1.PossibleNumbers[0]);
                                    n.PossibleNumbers.Contains(n1.PossibleNumbers[1]);
                                }

                                return 1;
                            }
                        }
                    }
                }
            }
        }

        return -1;
    }

    public int Tech04_PointingPairOrTriple()
    {
        for (int zone = 0; zone < 9; zone++)
        {
            List<int> usedNumbers = Nodes.Where(n => n.Zone == zone && n.Number.HasValue).Select(n => n.Number.Value).ToList();
            List<int> candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int number in usedNumbers) candidates.Remove(number);
            List<Node> nodesInZone = Nodes.Where(n => n.Zone == zone && !n.Number.HasValue).ToList();

            foreach (int number in candidates)
            {
                List<Node> nodes = nodesInZone.Where(n => n.PossibleNumbers.Contains(number)).ToList();
                if (nodes.Count > 3 || nodes.Count < 2) continue;

                for (int checkingtype = 0; checkingtype < 2; checkingtype++)
                {
                    bool isValid = true;
                    Node n1 = nodes[0];
                    for (int n2Index = 1; n2Index < nodes.Count; n2Index++)
                    {
                        Node n2 = nodes[n2Index];

                        if ((checkingtype == 0 ? n1.Row : n1.Column) != (checkingtype == 0 ? n2.Row : n2.Column))
                        {
                            // not fit, check next number; 
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        List<Node> otherNodesInSameRowOrSameColumn = 
                            Nodes.Where(n => n.Zone != zone 
                                && (checkingtype == 0 ? n.Row : n.Column) == (checkingtype == 0 ? n1.Row : n1.Column)
                                && n.PossibleNumbers.Contains(number)).ToList();
                        if (otherNodesInSameRowOrSameColumn.Count > 0)
                        {
                            // able to move forward
                            string stype = checkingtype == 0 ? "row": "column";
                            int itype = checkingtype == 0 ? n1.Row : n1.Column;
                            Console.WriteLine($"in zone {zone}, number {number} only appears in {stype} {itype}. Remove {number} from other cells in the same {stype}.");

                            foreach (Node n in otherNodesInSameRowOrSameColumn)
                                n.PossibleNumbers.Remove(number);

                            return 1;
                        }
                    }
                }
            }
        }

        return -1;
    }

    public int Tech05_ClaimingPairOrTriple() { return -1; }

    public int Tech06_NakedTripple() { return -1; }

    public int Tech07_XWing() { return -1; }

    public int Tech08_HiddePair() { return -1; }

    public int Tech09_NakesQuad() { return -1; }

    public int SolveStepByStep()
    {

        // techxx - solved game, return; solved 1 step, continue; cannot go further, next step
        List<System.Reflection.MethodInfo> solvingTechnics = this.GetType().GetMethods().ToList();

        int prevPossibleNumbersCount = this.CandidatesCount;
        int endResult = 0;

        while (true)
        {
            foreach (System.Reflection.MethodInfo mi in solvingTechnics)
            {
                if (mi.Name.StartsWith("Tech"))
                {
                    Console.Write($"Try one step of {mi.Name}: ");
                    int ret = (int)(mi.Invoke(this, null));
                    if (ret == 1) // moved forward
                    {
                        Console.WriteLine("moved forward, continue next step starting with technic 01.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Cannot move any further, try the next technic.");
                    }

                    // ret == -1, cannot go further, try next technic
                }
            }
            int possibleNumbersCount = this.CandidatesCount;
            if (possibleNumbersCount == prevPossibleNumbersCount)   // tried everything, cannot go any further
            {
                endResult = -1;
                break;
            }

            if (possibleNumbersCount == 0)      // game is solved
            {
                endResult = 0;
                break;
            }

            // moved forward, continue
            prevPossibleNumbersCount = possibleNumbersCount;
        }

        return endResult;
    }
}